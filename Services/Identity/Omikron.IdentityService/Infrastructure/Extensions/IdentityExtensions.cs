using System;
using System.Linq;
using System.Reflection;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.SecureVault;

namespace Omikron.IdentityService.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServerWithConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString(name: "IdentityServerConfigurationDb");

            services
                .AddDbContext<OmikronIdentityDbContext>(optionsAction: builder => builder
                    .UseSqlServer(connectionString: configuration.GetConnectionString(name: "IdentityServiceDatabase")));

            services.AddDbContext<IdentityPersistedGrantDbContext>();
            services.AddDbContext<IdentityConfigurationDbContext>();

            services.AddIdentity<User, Role>(setupAction: options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<OmikronIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddCertificate(configuration: configuration)
                .AddAspNetIdentity<User>()
                .AddConfigurationStore(storeOptionsAction: options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString: connectionString,
                            sqlServerOptionsAction: sql => sql.MigrationsAssembly(assemblyName: migrationsAssembly));
                })
                .AddOperationalStore(storeOptionsAction: options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString: connectionString,
                            sqlServerOptionsAction: sql => sql.MigrationsAssembly(assemblyName: migrationsAssembly));

                    options.EnableTokenCleanup = true;
                })
                .AddProfileService<MainProfileService>()
                .AddCustomTokenRequestValidator<TokenRequestValidator>();

            services.AddScoped<IUserStore<User>, EntityFrameworkIdentityUserStore>();
            services.AddScoped<IdentityUserManager>();
            services.AddScoped<IdentityRoleManager>();

            return services;
        }

        public static IApplicationBuilder UseIdentityServerAndApplyMigrations(this IApplicationBuilder app)
        {
            app.UseIdentityServer();

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<IdentityPersistedGrantDbContext>().Database.Migrate();

            serviceScope.ServiceProvider.GetRequiredService<OmikronIdentityDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<IdentityConfigurationDbContext>();
            context.Database.Migrate();

            AddClients(context: context);
            AddApiScopes(context: context);
            AddIdentityResources(context: context);
            AddApiResources(context: context);
            AddSystemUser(serviceScope: serviceScope);
            return app;
        }

        private static void AddSystemUser(IServiceScope serviceScope)
        {
            var dispatcher = serviceScope.ServiceProvider.GetService<IDispatcher>();
            dispatcher.DispatchAsync(command: new SetupSystemUserAccountCommand()).GetAwaiter().GetResult();
        }

        private static void AddClients(IdentityConfigurationDbContext context)
        {
            var clients = Config.GetClients().ToList();
            var clientsName = clients.Select(selector: x => x.ClientName).ToList();
            var existsClientsName = context.Clients.Where(predicate: x => clientsName.Contains(x.ClientName)).Select(selector: c => c.ClientName)
                .ToList();

            foreach (var client in clients.Where(predicate: c => !existsClientsName.Contains(item: c.ClientName)))
            {
                context.Clients.Add(entity: client.ToEntity());
            }

            context.SaveChanges();
        }

        private static void AddIdentityResources(IdentityConfigurationDbContext context)
        {
            var resources = Config.GetIdentityResources().ToList();
            var resourcesName = resources.Select(selector: x => x.Name).ToList();
            var existsResourcesName = context.IdentityResources.Where(predicate: x => resourcesName.Contains(x.Name)).Select(selector: c => c.Name)
                .ToList();

            foreach (var resource in resources.Where(predicate: c => !existsResourcesName.Contains(item: c.Name)))
            {
                context.IdentityResources.Add(entity: resource.ToEntity());
            }

            context.SaveChanges();
        }

        private static void AddApiResources(IdentityConfigurationDbContext context)
        {
            var resources = Config.GetApis().ToList();
            var resourcesName = resources.Select(selector: x => x.Name).ToList();
            var existsResourcesName = context.ApiResources.Where(predicate: x => resourcesName.Contains(x.Name)).Select(selector: c => c.Name)
                .ToList();

            foreach (var resource in resources.Where(predicate: c => !existsResourcesName.Contains(item: c.Name)))
            {
                context.ApiResources.Add(entity: resource.ToEntity());
            }

            context.SaveChanges();
        }

        private static void AddApiScopes(IdentityConfigurationDbContext context)
        {
            var scopes = Config.GetApiScope();
            var scopesName = scopes.Select(selector: x => x.Name).ToList();
            var existsResourcesName = context.ApiScopes.Where(predicate: x => scopesName.Contains(x.Name)).Select(selector: c => c.Name)
                .ToList();

            foreach (var resource in scopes.Where(predicate: c => !existsResourcesName.Contains(item: c.Name)))
            {
                context.ApiScopes.Add(entity: resource.ToEntity());
            }

            context.SaveChanges();
        }

        public static IIdentityServerBuilder AddCertificate(this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var environment = serviceProvider.GetService<IHostEnvironment>();

            if (environment == null)
            {
                return builder;
            }

            if (environment.IsLocal())
            {
                return builder.AddDeveloperSigningCredential();
            }

            var securedSecureVaultProvider = serviceProvider.GetService<ISecureVaultProvider>();
            var certificationName = configuration.GetValue<string>(key: "IdentityServer:CertificationName");
            var certificateOrNothing = securedSecureVaultProvider.GetCertificate(certificateName: certificationName);

            if (certificateOrNothing.HasNoValue)
            {
                throw new ArgumentNullException(paramName: certificationName);
            }

            builder.AddSigningCredential(certificate: certificateOrNothing.Value);
            return builder;
        }
    }
}