using IdentityServer4.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Omikron.IdentityService.Domain.Services;
using Omikron.IdentityService.Infrastructure;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Infrastructure.SmsProvider;
using Omikron.IdentityService.Models;
using Omikron.IdentityService.Repositories;
using Omikron.IdentityService.Repositories.Default;
using Omikron.IdentityService.UnitTest.Factories;
using Omikron.IdentityService.UnitTest.Services;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data;
using Omikron.SharedKernel.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Email;
using Omikron.SharedKernel.Infrastructure.Json;
using Omikron.SharedKernel.Infrastructure.Serialization.IoC;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Storage;
using Omikron.SharedKernel.Infrastructure.Tenants.Accessors;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using Omikron.TenantService.Domain.Services;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.IdentityService.UnitTest
{
    public class FixtureTest
    {
        public FixtureTest()
        {
            var serviceProvider = ConfigureServiceProvider();

            Dispatcher = serviceProvider.GetService<IDispatcher>();

            IdentityDbContext = serviceProvider.GetService<OmikronIdentityDbContext>();
            TenantStoreDbContext = serviceProvider.GetService<TenantStoreDbContext>();

            ConfigureDbContextDatabase(dbContext: IdentityDbContext);
            ConfigureDbContextDatabase(dbContext: TenantStoreDbContext);

            UserManager = serviceProvider.GetService<IdentityUserManager>();
            RoleManager = serviceProvider.GetService<IdentityRoleManager>();
            PhoneNumberRepository = serviceProvider.GetService<IPhoneNumberRepository>();

            AddSeedData();
        }

        public IDispatcher Dispatcher { get; }
        public OmikronIdentityDbContext IdentityDbContext { get; }
        public TenantStoreDbContext TenantStoreDbContext { get; }
        public IdentityUserManager UserManager { get; }
        public IdentityRoleManager RoleManager { get; }
        public IPhoneNumberRepository PhoneNumberRepository { get; }

        private static IServiceProvider ConfigureServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            var assemblies = new[] { Assembly.GetAssembly(type: typeof(Startup)) };

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            serviceCollection
                .AddSingleton<IConfiguration>(implementationInstance: configuration);

            serviceCollection.AddJsonSerialization()
                .UseMicrosoftSystemTextJsonProvider(options: DefaultJsonSerializerOptions.DefaultSerializerOptions);

            serviceCollection.AddCommandHandlers(assemblies: assemblies, configuration: configuration);

            serviceCollection
                .AddLogger(configuration: new ConfigurationRoot(providers: new List<IConfigurationProvider>()));

            serviceCollection
                .AddDomainEvents(assemblies: assemblies)
                .AddScoped<IEmailService, FakeEmailService>()
                .AddEmailContentFactories(assemblies: new[] { Assembly.GetExecutingAssembly() })
                .AddMapper(assembly: assemblies);

            var tenantAssessorByStaticStrategy = new TenantAccessorByStaticStrategy(
                tenantInfo: new OmikronTenantInfo(id: Tenant.SystemTenant.Id, identifier: Tenant.SystemTenant.Identifier, name: Tenant.SystemTenant.Name
                ));

            serviceCollection
                .AddTransient<ITenantAccessor>(implementationFactory: serviceProvider => tenantAssessorByStaticStrategy);

            AddDefaultTenant(serviceCollection: serviceCollection);
            AddDefaultContext(serviceCollection: serviceCollection);

            serviceCollection.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<OmikronIdentityDbContext>()
                .AddDefaultTokenProviders();

            serviceCollection.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(apiResources: Enumerable.Empty<ApiResource>())
                .AddInMemoryClients(clients: Enumerable.Empty<Client>())
                .AddInMemoryIdentityResources(identityResources: Enumerable.Empty<IdentityResource>())
                .AddProfileService<MainProfileService>();

            serviceCollection.AddTransient<IUserStore<User>, EntityFrameworkIdentityUserStore>();
            serviceCollection.AddTransient<IdentityUserManager>();

            serviceCollection
                .AddScoped<ITenantFactory<IdentityUserManager>, TestTenantUserManagerFactory>()
                .AddScoped<IUserAccountService, UserAccountService>()
                .AddScoped<ILocationLookupService, LocationLookupService>()
                .AddScoped<IPhoneNumberRepository, PhoneNumberRepository>()
                .AddScoped<IUserRepository, DefaultUserRepository>()
                .AddScoped<ISmsProvider, NullSmsProvider>()
                .AddScoped<IdentityRoleManager>()
                .AddScoped(implementationFactory: serviceProvider => new Mock<ITokenService>().Object);

            CreateStorageProviderMock(serviceCollection: serviceCollection);
            CreateBudApiServiceMock(serviceCollection: serviceCollection);
            CreateLocationLookupServiceMock(serviceCollection: serviceCollection);
            CreateHttpVaultServiceMock(serviceCollection: serviceCollection, configuration: configuration);

            serviceCollection.AddScoped(implementationFactory: serviceProvider => new Mock<IBus>().Object);
            serviceCollection.AddScoped(implementationFactory: serviceProvider => new Mock<IUserAccountService>().Object);

            var provider = serviceCollection.BuildServiceProvider();

            return provider;
        }

        private static void CreateStorageProviderMock(IServiceCollection serviceCollection)
        {
            var storageProviderMock = new Mock<IStorageProvider<Blob, Uri>>();
            storageProviderMock.Setup(expression: storageProvider => storageProvider.SaveAsync(It.IsAny<Blob>()))
                .Returns(value: Task.FromResult(result: new Uri(uriString: "https://identityservice.com/photo.jpg")));
            serviceCollection.AddScoped(implementationFactory: serviceProvider => storageProviderMock.Object);
        }

        private static void CreateHttpVaultServiceMock(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var uri = configuration.GetValue<string>(key: "Endpoint:VaultService:Uri");

            serviceCollection.AddHttpClient<IHttpVaultService, NullHttpVaultService>(
                    configureClient: client =>
                    {
                        client.BaseAddress = new Uri(uriString: uri);
                        client.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                    })
                .AddTransientHttpErrorPolicy(configurePolicy: builder => builder.WaitAndRetryAsync(sleepDurations: new[]
                {
                    TimeSpan.FromSeconds(value: 1),
                    TimeSpan.FromSeconds(value: 2),
                    TimeSpan.FromSeconds(value: 3)
                }));
        }

        private static void CreateBudApiServiceMock(IServiceCollection serviceCollection)
        {
            var budApiServiceMock = new Mock<IBudApiService>();

            budApiServiceMock.Setup(expression: x => x.PostToApi<BudBaseResponse<CreateCustomerResponse>>(BudApiEndpoints.CreateCustomer, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()).Result)
                .Returns(value: new BudBaseResponse<CreateCustomerResponse>
                {
                    Data = new CreateCustomerResponse
                    {
                        CustomerId = "64590D21-47D9-41C7-920D-3B597A09FE73",
                        CustomerSecret = "58F7D134-CF06-4826-A9D1-183A49D7592E"
                    }
                });

            budApiServiceMock.Setup(expression: x => x.GetFromApi<BudBaseResponse<IEnumerable<ObProvidersResponse>>>(BudApiEndpoints.ListObProviders, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()).Result)
                .Returns(value: new BudBaseResponse<IEnumerable<ObProvidersResponse>>
                {
                    Data = new List<ObProvidersResponse>
                    {
                        new ObProvidersResponse
                        {
                            Provider = "Lloyds_Sandbox",
                            DisplayName = "Lloyds Sandbox"
                        },
                        new ObProvidersResponse
                        {
                            Provider = "Natwest_Sandbox",
                            DisplayName = "Natwest Sandbox"
                        }
                    }
                });

            serviceCollection.AddScoped(implementationFactory: sp => budApiServiceMock.Object);
        }

        private static void CreateLocationLookupServiceMock(IServiceCollection serviceCollection)
		{
            var locationLookupServiceMock = new Mock<ILocationLookupService>();

            locationLookupServiceMock.Setup(x => x.GetLocationsByPostcodeExpanded("11101", It.IsAny<CancellationToken>()).Result)
                                     .Returns(new PostcodeExpandedResponse());

            serviceCollection.AddScoped(sp => locationLookupServiceMock.Object);
		}

        private static void AddDefaultContext(ServiceCollection serviceCollection)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();

            serviceCollection.AddDbContext<TenantStoreDbContext>(optionsAction: builder =>
                builder.UseSqlite(connectionString: connectionString), contextLifetime: ServiceLifetime.Singleton);
            serviceCollection.AddDbContext<IdentityPersistedGrantDbContext>(optionsAction: builder =>
                builder.UseSqlite(connectionString: connectionString), contextLifetime: ServiceLifetime.Singleton);
            serviceCollection.AddDbContext<IdentityConfigurationDbContext>(optionsAction: builder =>
                builder.UseSqlite(connectionString: connectionString), contextLifetime: ServiceLifetime.Singleton);
            serviceCollection.AddDbContext<OmikronIdentityDbContext>(optionsAction: builder =>
                builder.UseSqlite(connectionString: connectionString), contextLifetime: ServiceLifetime.Singleton);
        }

        private static void AddDefaultTenant(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(implementationFactory: serviceProvider => new OmikronTenantInfo(id: Tenant.SystemTenant.Id,
                identifier: Tenant.SystemTenant.Identifier, name: Tenant.SystemTenant.Name));
        }

        private static void ConfigureDbContextDatabase<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }

        private void AddSeedData()
        {
            var johnNumber = new PhoneNumber
            {
                Id = new Guid(g: "E8E4D083-615E-44AA-B314-818B4E01A5E8"),
                Number = "+453336663"
            };

            var selenaNumber = new PhoneNumber
            {
                Id = new Guid(g: "72B31FDD-C634-4808-A705-649576EE2CFC")
            };

            var dennisNumber = new PhoneNumber
            {
                Id = new Guid(g: "D474A58E-EFE9-4348-A2C8-384FFFCAE9CC")
            };

            var kennyNumber = new PhoneNumber
            {
                Id = new Guid(g: "5613C734-DBFB-41A0-A93D-068A9D6B46FA")
            };

            var marieNumber = new PhoneNumber
            {
                Id = new Guid(g: "84BC132E-B9BF-41BC-AD2A-23CEFE451522")
            };

            var felixNumber = new PhoneNumber
            {
                Id = new Guid(g: "A619578A-9053-4E66-B4DD-24DF9AA152FF")
            };

            var kenanNumber = new PhoneNumber
            {
                Id = new Guid(g: "C6F696FD-7AFD-43D8-95BC-ED5E0CE70B36")
            };

            var selmaNumber = new PhoneNumber
            {
                Id = new Guid(g: "99295A14-F25A-436F-B1CA-380D7D1F3DA3")
            };

            IdentityDbContext.PhoneNumbers.Add(entity: johnNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: selenaNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: dennisNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: kennyNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: marieNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: felixNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: kenanNumber);
            IdentityDbContext.PhoneNumbers.Add(entity: selmaNumber);

            var johnUser = new User
            {
                FirstName = "John",
                LastName = "Fox",
                PhoneNumber = "+45 333 6663",
                UserName = "john.fox@mail.com",
                Email = "john.fox@mail.com",
                ExternalId = new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
                PhoneNumberId = johnNumber.Id
            };
            UserManager.CreateAsync(user: johnUser).Wait();

            var selenaUser = new User
            {
                FirstName = "Selena",
                LastName = "Tesla",
                PhoneNumber = "+45 789 6663",
                UserName = "selena@mail.com",
                Email = "selena@mail.com",
                EmailConfirmed = true,
                ExternalId = new Guid(g: "2F8A6864-5136-43EA-BAC4-4886634A3908"),
                PhoneNumberId = selenaNumber.Id
            };
            UserManager.CreateAsync(user: selenaUser).Wait();

            var dennisUser = new User
            {
                FirstName = "Dennis",
                LastName = "Washington",
                PhoneNumber = "+45 789 6663",
                UserName = "Dennis@mail.com",
                Email = "Dennis@mail.com",
                EmailConfirmed = true,
                ExternalId = new Guid(g: "D390F43B-2F70-422B-BC91-09BE7726FB91"),
                ProfilePhoto = "photo.jpg",
                ConfirmationTokens = new List<ConfirmationToken>(collection: new[] { new ConfirmationToken(type: ConfirmationTokenType.ResetPassword, value: "A4625DF3-8CBF-4D3C-9016-DA6947C20A6B") }),
                PhoneNumberId = dennisNumber.Id
            };
            UserManager.CreateAsync(user: dennisUser, password: "6b!89o7Fa").Wait();

            var kennyUser = new User
            {
                FirstName = "Kenny",
                LastName = "Washington",
                PhoneNumber = "+45 789 6663",
                UserName = "kenny@mail.com",
                Email = "kenny@mail.com",
                EmailConfirmed = false,
                ExternalId = new Guid(g: "4E54BED3-729C-4B3A-A4D7-4C704EB81D29"),
                PhoneNumberId = kennyNumber.Id
            };
            UserManager.CreateAsync(user: kennyUser).Wait();

            var marieUser = new User
            {
                FirstName = "Marie",
                LastName = "Fox",
                PhoneNumber = "+45 112 3343 221",
                UserName = "marie@mail.com",
                Email = "marie@mail.com",
                EmailConfirmed = false,
                ExternalId = new Guid(g: "8F89A16F-CDC3-4675-A2C2-DF4AF0DAA329"),
                ConfirmationTokens = new List<ConfirmationToken>(collection: new[]
                {
                    new ConfirmationToken(type: ConfirmationTokenType.ConfirmationEmail,
                        value: "0AFFBC9C-8363-42C8-91C2-9D93DA06FA7B")
                }),
                PhoneNumberId = marieNumber.Id
            };
            UserManager.CreateAsync(user: marieUser).Wait();

            var felixUser = new User
            {
                FirstName = "Felix",
                LastName = "Fox",
                PhoneNumber = "+45 112 3343 221",
                UserName = "felix@mail.com",
                Email = "felix@mail.com",
                ExternalId = new Guid(g: "B3995EBC-CF81-4E4C-8247-56B79E1F0B40"),
                EmailConfirmed = false,
                PhoneNumberId = felixNumber.Id
            };
            UserManager.CreateAsync(user: felixUser).Wait();

            var kenanUser = new User
            {
                FirstName = "Kenan",
                LastName = "Badenez",
                PhoneNumber = "+45 112 3343 221",
                UserName = "kenan@mail.com",
                Email = "kenan@mail.com",
                ExternalId = new Guid(g: "742D4791-A984-4A65-AF8A-B44C76AB10CF"),
                EmailConfirmed = false,
                ConfirmationTokens = new List<ConfirmationToken>(collection: new[]
                {
                    new ConfirmationToken(type: ConfirmationTokenType.ConfirmationEmail,
                        value: "ABF75CE4-8645-4067-BEA6-C06A46AB6464")
                }),
                PhoneNumberId = kenanNumber.Id
            };
            UserManager.CreateAsync(user: kenanUser).Wait();

            var selmaUser = new User
            {
                FirstName = "Selma",
                LastName = "Badenez",
                PhoneNumber = "+45 112 3343 221",
                UserName = "selma@mail.com",
                Email = "selma@mail.com",
                ExternalId = new Guid(g: "6B056258-EC08-42A2-8CB8-6BDE1A37DDEA"),
                EmailConfirmed = false,
                PhoneNumberId = selmaNumber.Id
            };
            UserManager.CreateAsync(user: selmaUser).Wait();

            RoleManager.CreateAsync(role: new Role { Name = "Test.Role", ExternalId = new Guid(g: "751672AA-5865-4D50-BE3A-5371D485B2DC"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo", ExternalId = new Guid(g: "15DE9097-04A5-492F-B5D4-3ACC06D09F5B"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-2", ExternalId = new Guid(g: "85C3BAD7-C26F-4F1D-910E-C9FD10C08AFC"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-3", ExternalId = new Guid(g: "BE5093ED-8F37-45FC-A75F-95EC7C937D11"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-4", ExternalId = new Guid(g: "FC7F8E41-CE21-4DE5-911D-95EC7C917D11"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-5", ExternalId = new Guid(g: "E9182E44-4770-42DF-BAB1-7F00AE7AE5EB"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-6", ExternalId = new Guid(g: "3DAFDFCA-6E4C-4E93-819A-CF60F3830D1B"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-7", ExternalId = new Guid(g: "9DBE4C0E-F1A0-4AF5-AC95-4AA0ADD3F676"), Description = "The test role for tests." }).Wait();
            RoleManager.CreateAsync(role: new Role { Name = "Test.Role.Demo-8", ExternalId = new Guid(g: "EBEE0A84-12F8-4CE7-B014-A507377CC8EF"), Description = "The test role for tests." }).Wait();

            UserManager.AddToRoleAsync(user: dennisUser, role: RoleConstants.SystemTenantAdministratorRole).Wait();
            UserManager.AddToRoleAsync(user: dennisUser, role: "Test.Role.Demo-4").Wait();
            UserManager.AddToRoleAsync(user: dennisUser, role: "Test.Role.Demo-5").Wait();
            UserManager.AddToRolesAsync(user: selmaUser, roles: new[] { "Test.Role.Demo-5", "Test.Role.Demo-6", "Test.Role.Demo-7" }).Wait();
        }
    }

    [CollectionDefinition(name: "Database collection")]
    public class DatabaseCollection : ICollectionFixture<FixtureTest>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}