using MassTransit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.VaultService.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Omikron.VaultService.UnitTest.ManualAccountDetails
{
	public class ManualAccountDetailsFixture
	{
		public ManualAccountDetailsFixture()
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
		public IPersonalItemRepository PersonalItemRepository { get; }
		public IInvestmentRepository InvestmentRepository { get; }
		public VaultServiceDatabaseContext VaultServiceDbContext { get; }


		private static IServiceProvider ConfigureServiceProvider()
		{
			var assemblies = new[] { Assembly.GetAssembly(type: typeof(Startup)) };

			var serviceCollection = new ServiceCollection();

			serviceCollection.AddCommandHandlers(assemblies: assemblies,
				configuration: new ConfigurationRoot(providers: new List<IConfigurationProvider>()));
			serviceCollection
				.AddScoped(implementationFactory: serviceProvider => new Mock<IBus>().Object)
				.AddScoped<IAccountRepository, AccountRepository>()
				.AddScoped<IPropertyRepository, PropertyRepository>()
				.AddScoped<IRefreshHistoryRepository, RefreshHistoryRepository>()
				.AddScoped<IVaultItemRepository, VaultItemRepository>()
				.AddScoped<IPersonalItemRepository, PersonalItemRepository>()
				.AddScoped<IVehicleRepository, VehicleRepository>()
				.AddScoped<IInvestmentRepository, InvestmentRepository>()
				.AddScoped<IPersonalItemRepository, PersonalItemRepository>()
				.AddScoped<IVehicleValueRepository, VehicleValueRepository>()
				.AddScoped<IPropertyValueRepository, PropertyValueRepository>()
				.AddScoped<IPersonalItemValueRepository, PersonalItemValueRepository>()
				.AddScoped<ITransactionRepository, TransactionRepository>()
				.AddScoped<IInvestmentValuesRepository, InvestmentValuesRepository>()
				.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();

			serviceCollection
				.AddMapper(assemblies)
				.AddServices();

			const string marker = "IObservable";

			serviceCollection.AddServicesAsImplementedInterface(marker: marker, lifetime: ServiceLifetime.Transient,
					assemblies: assemblies)
				.AddLogger(configuration: new ConfigurationRoot(providers: new List<IConfigurationProvider>()));

			serviceCollection
				.AddDataRepository(assemblies: assemblies);

			AddDefaultContext(serviceCollection: serviceCollection);

			return serviceCollection.BuildServiceProvider();
		}

		private static void AddDefaultContext(ServiceCollection serviceCollection)
		{
			var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
			var connectionString = connectionStringBuilder.ToString();

			serviceCollection
				.AddDbContext<VaultServiceDatabaseContext>(
					optionsAction: builder => builder.UseSqlite(connectionString: connectionString),
					contextLifetime: ServiceLifetime.Singleton);
		}

		private static void ConfigureDbContextDatabase<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
		{
			dbContext.Database.OpenConnection();
			dbContext.Database.EnsureCreated();
		}

		private void AddSeedData()
		{
			SeedProperty();
			SeedVehicle();
			SeedInvestment();
			SeedPersonalItem();
			SeedManualAccounts();

			VaultServiceDbContext.SaveChanges();
		}

		private void SeedProperty()
		{
			var manualAccountProperty = new Property()
			{
				Id = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Manual Account Test Property",
				NumberOfBedrooms = 2,
				Postcode = "ABC1 1ZY",
				Address = "This is the property address",
				PropertyValues = new List<PropertyValue>()
				{
					new() { Amount = 123000m, CreatedAt = new DateTime(2021, 1, 1) },
					new() { Amount = 11000m, CreatedAt = new DateTime(2021, 2, 2) },
				},
				Mortgage = new Account()
				{
					Id = new Guid("e1a2ac29-0f0c-49d0-9866-3b2344ae0d1b"),
					Source = AccountSource.Manual,
					Name = "Mortgage",
					Provider = "Lloyds",
					Type = AccountType.Loan,
					Currency = "GBP",
					ExternalId = Guid.NewGuid(),
					OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
					Notes = "This is some notes",
					ReferenceNumber = "Ref No.",
					Transactions = new List<Transaction>()
					{
						new()
						{
							Amount = 200m, Date = new DateTime(2021, 3, 1), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
						new()
						{
							Amount = 100m, Date = new DateTime(2021, 3, 2), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
					},
					AccountBalances = new List<AccountBalance>()
					{
						new()
						{
							Amount = 200m, EntryDate = new DateTime(2021, 3, 1, 10, 1, 1), CreatedAt = new DateTime(2021, 3, 1), BalanceType = Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit
						},
						new()
						{
							Amount = 100m, EntryDate = new DateTime(2021, 3, 1, 11, 1, 1), CreatedAt = new DateTime(2021, 3, 2), BalanceType = Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit
						},
					}
				}
			};

			VaultServiceDbContext.Properties.Add(manualAccountProperty);

			var vaultItem = new VaultItem() { HostId = manualAccountProperty.Id, OwnerId = manualAccountProperty.OwnerId, ItemType = VaultItemType.Property, CreditDebitIndicator = CreditDebitIndicator.Credit };
			VaultServiceDbContext.VaultItems.Add(vaultItem);
		}

		private void SeedVehicle()
		{
			var manualAccountVehicle = new Vehicle()
			{
				Id = new Guid("c5682a34-819f-4123-94d4-2963ae350981"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Manual Account Test Vehicle",
				Currency = "GBP",
				ExternalId = new Guid(g: "ea10fb85-f97f-4850-a1ee-bade9efe79b2"),
				Mileage = "7482",
				Registration = "ABC",
				VehicleValues = new List<VehicleValue>()
				{
					new VehicleValue() { Amount = 2200m, CreatedAt = new DateTime(2021, 2, 1) },
					new VehicleValue() { Amount = 2000m, CreatedAt = new DateTime(2021, 2, 2) },
				},
				FinancialAgreement = new Account()
				{
					Id = new Guid("5c28fd95-aa61-4422-b514-544faa1bb1a6"),
					Source = AccountSource.Manual,
					Name = "Financial Agreement",
					Provider = "Lloyds",
					Type = AccountType.Loan,
					Currency = "GBP",
					ExternalId = new Guid("e5887d87-38c3-4763-8a8e-54379ef345fb"),
					OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
					Notes = "This is some notes",
					ReferenceNumber = "Ref No.",
					Transactions = new List<Transaction>()
					{
						new()
						{
							Amount = 200m, Date = new DateTime(2021, 3, 1), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
						new()
						{
							Amount = 100m, Date = new DateTime(2021, 3, 2), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
					},
					AccountBalances = new List<AccountBalance>()
					{
						new()
						{
							Amount = 200m, EntryDate = new DateTime(2021, 3, 1, 10, 1, 1), CreatedAt = new DateTime(2021, 3, 1), BalanceType = Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit
						},
						new()
						{
							Amount = 100m, EntryDate = new DateTime(2021, 3, 1, 11, 1, 1), CreatedAt = new DateTime(2021, 3, 2), BalanceType = Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit
						},
					}
				}
			};

			VaultServiceDbContext.Vehicles.Add(manualAccountVehicle);

			var vehicleWithoutFinance = new Vehicle()
			{
				Id = new Guid("f5682a34-819f-4123-94d4-2963ae350982"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				Name = "Manual Account Without Finance",
				Currency = "GBP",
				ExternalId = new Guid(g: "ea10fb85-f97f-4850-a1ee-bade9efe79b2"),
				Mileage = "7482",
				Registration = "ABC",
				VehicleValues = new List<VehicleValue>()
				{
					new VehicleValue() { Amount = 2200m, CreatedAt = new DateTime(2021, 2, 1) },
					new VehicleValue() { Amount = 2000m, CreatedAt = new DateTime(2021, 2, 2) },
				},
			};

			VaultServiceDbContext.Vehicles.Add(vehicleWithoutFinance);

			var vaultItem = new VaultItem() { HostId = manualAccountVehicle.Id, OwnerId = manualAccountVehicle.OwnerId, ItemType = VaultItemType.Vehicle, CreditDebitIndicator = CreditDebitIndicator.Credit };
			VaultServiceDbContext.VaultItems.Add(vaultItem);
		}

		private void SeedInvestment()
		{
			var investment = new Investment
			{
				CreatedAt = default,
				ModifiedAt = null,
				Id = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				ExternalId = default,
				AutomaticallyRevalueInvestment = true,
				InvestmentValues = new List<InvestmentValue>()
				{
					new() { Amount = 200m, CreatedAt = new DateTime(2021, 2, 1), EntryDate = new DateTime(2021, 2, 1) },
					new() { Amount = 100m, CreatedAt = new DateTime(2021, 2, 3), EntryDate = new DateTime(2021, 2, 3) }
				},
				Category = "Investment",
				Currency = "Dollar",
				Name = "Test",
				ImageUrl = null,
				TickerCode = "Code",
				UnitPrice = 10,
				Quantity = 1000,
				TotalValue = 230,
			};

			VaultServiceDbContext.Investments.Add(investment);

			var vaultItem = new VaultItem() { HostId = investment.Id, OwnerId = investment.OwnerId, ItemType = VaultItemType.Investment, CreditDebitIndicator = CreditDebitIndicator.Credit };
			VaultServiceDbContext.VaultItems.Add(vaultItem);
		}

		private void SeedPersonalItem()
		{
			var personalItem = new PersonalItem()
			{
				Name = "Personal Item",
				Id = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"),
				OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
				PersonalItemValues = new List<PersonalItemValue>()
				{
					new PersonalItemValue() { Amount = 2200m, CreatedAt = new DateTime(2021, 2, 1) },
					new PersonalItemValue() { Amount = 2000m, CreatedAt = new DateTime(2021, 2, 2) },
				},
				FinancialAgreement = new Account()
				{
					Id = new Guid("1d9c3b59-3c98-4ea2-b31a-4960e227524d"),
					Source = AccountSource.Manual,
					Name = "Financial Agreement",
					Provider = "Lloyds",
					Type = AccountType.Loan,
					Currency = "GBP",
					ExternalId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"),
					OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
					Notes = "This is some notes",
					ReferenceNumber = "Ref No.",
					Transactions = new List<Transaction>()
					{
						new()
						{
							Amount = 200m, Date = new DateTime(2021, 3, 1), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
						new()
						{
							Amount = 100m, Date = new DateTime(2021, 3, 2), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
					},
					AccountBalances = new List<AccountBalance>()
					{
						new()
						{
							Amount = 200m, CreatedAt = new DateTime(2021, 3, 1), BalanceType = Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit
						},
						new()
						{
							Amount = 100m, CreatedAt = new DateTime(2021, 3, 2), BalanceType = Constants.PrimaryBalanceType, CreditDebitIndicator = CreditDebitIndicator.Debit
						},
					}
				}
			};

			VaultServiceDbContext.PersonalItems.Add(personalItem);

			var vaultItem = new VaultItem() { HostId = personalItem.Id, OwnerId = personalItem.OwnerId, ItemType = VaultItemType.PersonalItem, CreditDebitIndicator = CreditDebitIndicator.Credit };
			VaultServiceDbContext.VaultItems.Add(vaultItem);
		}

		private void SeedManualAccounts()
		{
			var accounts = new List<(Guid, string)>()
			{
				(new Guid("3087c40e-4281-4636-bfed-fc1d3a499a26"), "CurrentAccount"),
				(new Guid("b59df78a-5db7-4a2a-b5b4-7eb4573be066"), "SavingsAccount"),
				(new Guid("e7997e74-13e7-4ee1-8788-edd7dc19737b"), "CreditCard"),
				(new Guid("1d371241-00b5-4cc9-8bed-5d2e2cf4a3d2"), "Loan"),
				(new Guid("2a999a77-d28f-4826-a384-88baf3de502a"), "Pension"),
			};

			foreach (var valueTuple in accounts)
			{
				var account = new Account()
				{
					Id = valueTuple.Item1,
					Source = AccountSource.Manual,
					Name = valueTuple.Item2,
					Provider = "Lloyds",
					Type = AccountType.Loan,
					Currency = "GBP",
					ExternalId = valueTuple.Item1,
					OwnerId = CustomerId.Parse(id: new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF")),
					Notes = "This is some notes",
					ReferenceNumber = "Ref No.",
					Transactions = new List<Transaction>()
					{
						new()
						{
							Amount = 200m, Date = new DateTime(2021, 3, 1), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
						new()
						{
							Amount = 100m, Date = new DateTime(2021, 3, 2), Category = "Value Change", Currency = Constants.DefaultCurrencyCode
						},
					},
					AccountBalances = new List<AccountBalance>()
					{
						new()
						{
							Amount = 200m,
							EntryDate = new DateTime(2021, 3, 1, 10, 1, 1),
							CreatedAt = new DateTime(2021, 3, 1),
							BalanceType = Constants.PrimaryBalanceType,
							CreditDebitIndicator = CreditDebitIndicator.Debit
						},
						new()
						{
							Amount = 100m,
							EntryDate = new DateTime(2021, 3, 1, 11, 1, 1),
							CreatedAt = new DateTime(2021, 3, 2),
							BalanceType = Constants.PrimaryBalanceType,
							CreditDebitIndicator = CreditDebitIndicator.Debit
						},
					}
				};

				VaultServiceDbContext.Accounts.Add(account);

				var vaultItem = new VaultItem() { HostId = account.Id, OwnerId = account.OwnerId, ItemType = VaultItemType.Account, CreditDebitIndicator = CreditDebitIndicator.Debit };
				VaultServiceDbContext.VaultItems.Add(vaultItem);
			}
		}
	}
}