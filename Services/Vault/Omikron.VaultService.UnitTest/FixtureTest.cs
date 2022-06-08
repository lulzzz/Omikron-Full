using MassTransit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Json;
using Omikron.SharedKernel.Infrastructure.Serialization.IoC;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Omikron.VaultService.UnitTest
{
	public class FixtureTest
	{
		public FixtureTest()
		{
			Provider = ConfigureServiceProvider();
			Dispatcher = Provider.GetService<IDispatcher>();
			AccountService = Provider.GetService<IAccountService>();
			VaultServiceDbContext = Provider.GetService<VaultServiceDatabaseContext>();
			AccountRepository = Provider.GetService<IAccountRepository>();
			PropertyRepository = Provider.GetService<IPropertyRepository>();
			RefreshHistoryRepository = Provider.GetService<IRefreshHistoryRepository>();
			VaultItemRepository = Provider.GetService<IVaultItemRepository>();
			VehicleRepository = Provider.GetService<IVehicleRepository>();
			PersonalItemRepository = Provider.GetService<IPersonalItemRepository>();
			InvestmentRepository = Provider.GetService<IInvestmentRepository>();

			ConfigureDbContextDatabase(dbContext: VaultServiceDbContext);

			AddSeedData();
		}

		public IServiceProvider Provider { get; }
		public IDispatcher Dispatcher { get; }
		public IAccountService AccountService { get; }
		public IAccountRepository AccountRepository { get; }
		public IPropertyRepository PropertyRepository { get; }
		public IRefreshHistoryRepository RefreshHistoryRepository { get; }
		public IVaultItemRepository VaultItemRepository { get; }
		public IVehicleRepository VehicleRepository { get; }
		public IVehicleValueRepository VehicleValueRepository { get; }
		public IPersonalItemRepository PersonalItemRepository { get; }
		public IInvestmentRepository InvestmentRepository { get; }
		public IAccountBalanceRepository AccountBalanceRepository { get; }
		public VaultServiceDatabaseContext VaultServiceDbContext { get; }


		private static IServiceProvider ConfigureServiceProvider()
		{
			var assemblies = new[] { Assembly.GetAssembly(type: typeof(Startup)) };

			var configuration = new ConfigurationBuilder()
				.SetBasePath(basePath: Directory.GetCurrentDirectory())
				.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			var serviceCollection = new ServiceCollection();

			serviceCollection.AddCommandHandlers(assemblies: assemblies, configuration: new ConfigurationRoot(providers: new List<IConfigurationProvider>()));
			serviceCollection
				.AddScoped(implementationFactory: serviceProvider => new Mock<IBus>().Object)
				.AddScoped<IAccountRepository, AccountRepository>()
				.AddScoped<IPropertyRepository, PropertyRepository>()
				.AddScoped<IRefreshHistoryRepository, RefreshHistoryRepository>()
				.AddScoped<IVaultItemRepository, VaultItemRepository>()
				.AddScoped<IPersonalItemRepository, PersonalItemRepository>()
				.AddScoped<IVehicleRepository, VehicleRepository>()
				.AddScoped<IVehicleValueRepository, VehicleValueRepository>()
				.AddScoped<IInvestmentRepository, InvestmentRepository>()
				.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>()
				.AddScoped<IPropertyValueRepository, PropertyValueRepository>()
				.AddScoped(implementationFactory: serviceProvider => new Mock<ITokenService>().Object);

			CreateHttpIdentityServiceMock(serviceCollection: serviceCollection, configuration: configuration);

			serviceCollection
				.AddServices()
				.AddMapper(assemblies);

			const string marker = "IObservable";

			serviceCollection.AddServicesAsImplementedInterface(marker: marker, lifetime: ServiceLifetime.Transient, assemblies: assemblies)
				.AddLogger(configuration: new ConfigurationRoot(providers: new List<IConfigurationProvider>()));

			serviceCollection.AddJsonSerialization()
				.UseMicrosoftSystemTextJsonProvider(options: DefaultJsonSerializerOptions.DefaultSerializerOptions);

			serviceCollection
				.AddDataRepository(assemblies: assemblies);

			AddDefaultContext(serviceCollection: serviceCollection);

			return serviceCollection.BuildServiceProvider();
		}

		private static void CreateHttpIdentityServiceMock(IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var uri = configuration.GetValue<string>(key: "Endpoint:IdentityService:Uri");
			var identityServiceMock = new Mock<IHttpIdentityService>();

			identityServiceMock.Setup(x => x.GetUserRegistrationDate<UserRegistrationDateResponse>(It.IsAny<CancellationToken>()).Result)
				.Returns(new ApiResult<UserRegistrationDateResponse>()
				{
					Records = new()
					{
						Day = 1,
						Month = 1,
						Year = 1
					}
				});

			serviceCollection.AddScoped(sp => identityServiceMock.Object);
		}

		private static void AddDefaultContext(ServiceCollection serviceCollection)
		{
			var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
			var connectionString = connectionStringBuilder.ToString();

			serviceCollection
				.AddDbContext<VaultServiceDatabaseContext>(optionsAction: builder => builder.UseSqlite(connectionString: connectionString), contextLifetime: ServiceLifetime.Singleton);
		}

		private static void ConfigureDbContextDatabase<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
		{
			dbContext.Database.OpenConnection();
			dbContext.Database.EnsureCreated();
		}

		private void AddSeedData()
		{
			var savingsAccount = new Account
			{
				Id = new Guid(g: "10AB198B-1435-483B-80E8-3B0A2BBDAEAB"),
				Source = AccountSource.Manual,
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Test Account",
				Provider = "Lloyds",
				Type = AccountType.Savings,
				Currency = "GBP",
				ExternalId = new Guid(g: "10AB198B-1435-483B-80E8-3B0A2BBDAEAB")
			};
			VaultServiceDbContext.Accounts.Add(entity: savingsAccount);

			var loanAccount = new Account
			{
				Id = new Guid(g: "10AB198B-1435-483B-80E8-3B0A2BBDACCC"),
				Source = AccountSource.Manual,
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Loan test Account",
				Provider = "Lloyds",
				Type = AccountType.Loan,
				Currency = "GBP",
				ExternalId = new Guid(g: "10AB198B-1435-483B-80E8-3B0A2BBDAEAB"),
				AccountBalances = new List<AccountBalance>()
				{
					new() {Amount = 700, AccountId = new Guid(g: "10AB198B-1435-483B-80E8-3B0A2BBDACCC"), EntryDate=Clock.GetTime(), BalanceType=Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit}
				}
			};
			VaultServiceDbContext.Accounts.Add(entity: loanAccount);

			var property = new Property
			{
				Id = new Guid(g: "AE3CBC31-F900-4011-A2C6-4F2EEEDD129C"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Test Property",
				Currency = "GBP",
				ExternalId = new Guid(g: "AE3CBC31-F900-4011-A2C6-4F2EEEDD129C"),
				PropertyValues = new List<PropertyValue>()
				{
					new() {Amount = 1000, PropertyId = new Guid(g: "AE3CBC31-F900-4011-A2C6-4F2EEEDD129C"), EntryDate=Clock.GetTime()}
				}
			};
			VaultServiceDbContext.Properties.Add(entity: property);

			var vehicle = new Vehicle
			{
				Id = new Guid(g: "D5781540-E2D4-4D4D-B66A-1C8B061C5CEB"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Test Vehicle",
				Currency = "GBP",
				ExternalId = new Guid(g: "D5781540-E2D4-4D4D-B66A-1C8B061C5CEB"),
				Registration = "Reg",
				Mileage = "20",
				VehicleValues = new List<VehicleValue>()
				{
					new() {Amount = 200, VehicleId = new Guid(g: "D5781540-E2D4-4D4D-B66A-1C8B061C5CEB"), EntryDate=Clock.GetTime()}
				}
			};
			VaultServiceDbContext.Vehicles.Add(entity: vehicle);

			var investment = new Investment
			{
				Id = new Guid(g: "D5781540-ABBB-4D4D-B66A-1C8B061C5CEB"),
				Quantity = 1,
				UnitPrice = 1,
				TotalValue = 1,
				TickerCode = "Ticker",
				Category = "Category",
				Name = "Investment",
				AutomaticallyRevalueInvestment = false,
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				InvestmentValues = new List<InvestmentValue>()
				{
					new() {Amount = 200, InvestmentId = new Guid(g: "D5781540-ABBB-4D4D-B66A-1C8B061C5CEB"), EntryDate=Clock.GetTime()}
				}
			};
			VaultServiceDbContext.Investments.Add(entity: investment);

			var personalItem = new PersonalItem()
			{
				Id = new Guid("3D78282E-0CA6-4C49-B4F0-EB2B9D9FD6B1"),
				OwnerId = CustomerId.Parse(new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Test Personal Item",
				Currency = "GBP",
				ExternalId = new Guid("3D78282E-0CA6-4C49-B4F0-EB2B9D9FD6B1"),
			};
			VaultServiceDbContext.PersonalItems.Add(personalItem);

			var vaultItem = new VaultItem()
			{
				Id = new Guid(g: "1B0A2D86-3F26-4C5D-86C0-EAC5FF507CFD"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Test Vehicle",
				Value = decimal.Zero,
				ItemType = VaultItemType.Vehicle
			};
			VaultServiceDbContext.VaultItems.Add(entity: vaultItem);

			var refreshHistory = new RefreshHistory(userId: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF"));
			VaultServiceDbContext.RefreshHistories.Add(entity: refreshHistory);

			VaultServiceDbContext.SaveChanges();
		}
	}
}