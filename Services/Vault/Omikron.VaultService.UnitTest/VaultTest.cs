using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.VaultService.UnitTest
{
    public class VaultTest : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _fixture;

        public VaultTest(FixtureTest fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CalculateTotalBalances_Should_Return_Correct_Balance()
        {
            //Arrange
            var accounts = new List<Account>()
            {
                new()
                {
                    Type = AccountType.Savings,
                    AccountBalances = new List<AccountBalance>()
                    {
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 100m
                        },
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime() - TimeSpan.FromDays(3),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 300m
                        }
                    }
                },
                new ()
                {
                    Type = AccountType.Savings,
                    AccountBalances = new List<AccountBalance>()
                    {
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 100m
                        },
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime() - TimeSpan.FromDays(3),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 300m
                        }
                    }
                }
            };

            //Act
            var result = _fixture.AccountService.CalculateTotalBalance(accounts);

            //Assert
            result.Should().Be(200m);
        }

        [Fact]
        public void GetLastBalance_Should_Return_Last_Balance()
        {
            //Arrange
            var account = new Account()
            {
                Type = AccountType.Savings,
                AccountBalances = new List<AccountBalance>()
                {
                    new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 100m
                        },
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime() - TimeSpan.FromDays(3),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 300m
                        }
                }
            };

            //Act
            var result = _fixture.AccountService.GetLastBalance(account);
            var lastBalanceAmount = result != null ? result.Amount : 0;

            //Assert
            lastBalanceAmount.Should().Be(100m);
        }

        [Fact]
        public void GetTotalAssetsAndLiabilities_Should_Return_Correct_Result()
        {
            //Arrange
            var accounts = new List<Account>()
            {
                new()
                {
                    Type = AccountType.Savings,
                    AccountBalances = new List<AccountBalance>()
                    {
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 50m
                        },
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            EntryDate = Clock.GetTime() - TimeSpan.FromDays(3),
                            Amount = 300m
                        }
                    }
                },
                new ()
                {
                    Type = AccountType.Savings,
                    AccountBalances = new List<AccountBalance>()
                    {
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            Amount = 100m
                        },
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            CreditDebitIndicator = CreditDebitIndicator.Credit,
                            EntryDate = Clock.GetTime() - TimeSpan.FromDays(3),
                            Amount = 300m
                        }
                    }
                },
                new ()
                {
                    Type = AccountType.CreditCard,
                    AccountBalances = new List<AccountBalance>()
                    {
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Debit,
                            Amount = 100m
                        },
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime() - TimeSpan.FromDays(3),
                            CreditDebitIndicator = CreditDebitIndicator.Debit,
                            Amount = 300m
                        }
                    }
                },
                new ()
                {
                    Type = AccountType.CreditCard,
                    AccountBalances = new List<AccountBalance>()
                    {
                        new()
                        {
                            BalanceType = Constants.PrimaryBalanceType,
                            EntryDate = Clock.GetTime(),
                            CreditDebitIndicator = CreditDebitIndicator.Debit,
                            Amount = 100m
                        }
                    }
                }
            };

            //Act
            var result = _fixture.AccountService.GetTotalAssetsAndLiabilities(accounts);

            //Assert
            result.Assets.Should().Be(150m);
            result.Liabilities.Should().Be(-200m);
            result.Net.Should().Be(-50m);
        }

       

        [Fact]
        public async Task AddVehicle_Should_Create_New_Vehicle()
        {
            //Arrange
            var command = new AddVehicle.Command()
            {
                AutomaticallyReValueVehicle = true,
                Mileage = 10,
                Registration = "ABC123",
                UserId = new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
                VehicleName = "Vehicle",
                VehicleValue = 123
            };
            var ownerId = CustomerId.Parse(command.UserId);
            
            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.Should().NotBe(null);

            var vehicle = await _fixture.VaultServiceDbContext.Vehicles.Include(x => x.VehicleValues).OrderBy(x=> x.CreatedAt).LastOrDefaultAsync(x => x.OwnerId == ownerId);

            vehicle.Should().NotBe(null);
            vehicle.Registration.Should().Be(command.Registration);
            vehicle.Mileage.Should().Be(command.Mileage.ToString());
            vehicle.AutomaticallyReValueVehicle.Should().Be(command.AutomaticallyReValueVehicle);
            vehicle.Name.Should().Be(command.VehicleName);
            vehicle.VehicleValues.First().Amount.Should().Be(command.VehicleValue);
            vehicle.OwnerId.Id.Should().Be(ownerId);
        }

        [Fact]
        public async Task AddInvestment_Should_Create_New_Investment()
        {
            
            var command = new AddInvestment.Command()
            {
                AutomaticallyRevalueInvestment = false,
                Category = "Category",
                InvestmentName = "Investment",
                TickerCode = "Ticker",
                Quantity = 1,
                UnitPrice = 1,
                TotalValue = 1,
                OwnerId = new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
            };

            var ownerId = CustomerId.Parse(command.OwnerId);
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.Should().NotBe(null);

            var investment = await _fixture.VaultServiceDbContext.Investments.Include(x => x.InvestmentValues).OrderBy(x=> x.CreatedAt).LastOrDefaultAsync(x => x.OwnerId == ownerId);

            investment.Should().NotBe(null);
            investment.Name.Should().Be(command.InvestmentName);
            investment.Category.Should().Be(command.Category.ToString());
            investment.Quantity.Should().Be(command.Quantity);
            investment.UnitPrice.Should().Be(command.UnitPrice);
            investment.TotalValue.Should().Be(command.TotalValue);
            investment.OwnerId.Id.Should().Be(ownerId);
            investment.InvestmentValues.First().Amount.Should().Be(command.TotalValue);
            investment.AutomaticallyRevalueInvestment.Should().Be(command.AutomaticallyRevalueInvestment);
        }

        [Fact]
        public async Task AddInvestment_Should_Throw_Bad_Request_If_Investment_Name_Exists()
        {
            //Arrange
            var command = new AddInvestment.Command()
            {
                AutomaticallyRevalueInvestment = false,
                InvestmentName = "Investment",
                Category = "Category",
                TickerCode = "Ticker",
                Quantity = 1,
                UnitPrice = 1,
                TotalValue = 1,
                OwnerId = new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddManualAccount_Should_Create_New_Loan_Account()
        {

            var command = new AddManualAccount.Command()
            {
                Name = "Loan test",
                Balance = 100,
                Provider = "Loan",
                Type = AccountType.Loan.Id,
                OwnerId = new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
            };

            var ownerId = CustomerId.Parse(command.OwnerId);
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.Should().NotBe(null);

            var loan = await _fixture.VaultServiceDbContext.Accounts.Include(x => x.AccountBalances).OrderBy(x => x.CreatedAt).LastOrDefaultAsync(x => x.OwnerId == ownerId);

            loan.Should().NotBe(null);
            loan.Name.Should().Be(command.Name);
            loan.OwnerId.Id.Should().Be(ownerId);
            loan.AccountBalances.First().Amount.Should().Be(command.Balance);
        }

        [Fact]
        public async Task AddManualAccount_Should_Throw_Bad_Request_If_Loan_Name_Exists()
        {
            //Arrange
            var command = new AddManualAccount.Command()
            {
                Name = "Loan test",
                Balance = 100,
                Provider = "Loan",
                Type = AccountType.Loan.Id,
                OwnerId = new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddManualAccount_Should_Throw_Bad_Request_If_Loan_Name_Exists_Case_Sensitive()
        {
            //Arrange
            var command = new AddManualAccount.Command()
            {
                Name = "Loan TEST",
                Balance = 100,
                Provider = "Loan",
                Type = AccountType.Loan.Id,
                OwnerId = new Guid("43FE198B-1435-483B-80E8-3B0A2BBDAEDF"),
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddProperty_Should_Create_New_Property()
        {
            //Arrage
            var command = new AddProperty.Command()
            {
                AutomaticallyReValueProperty = true,
                NumberOfBedrooms = 23,
                Postcode = "ABC12",
                PropertyName = "Name",
                PropertyValue = 1234,
                UserId = "43FE198B-1435-483B-80E8-3B0A2BBDAEDF",
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.Should().NotBe(null);

            var property = await _fixture.VaultServiceDbContext.Properties.Include(x => x.PropertyValues).SingleOrDefaultAsync(x => x.OwnerId == command.UserId);

            property.Should().NotBe(null);

            property.AutomaticallyReValueProperty.Should().Be(command.AutomaticallyReValueProperty);
            property.NumberOfBedrooms.Should().Be(command.NumberOfBedrooms);
            property.Postcode.Should().Be(command.Postcode);
            property.Name.Should().Be(command.PropertyName);
            property.OwnerId.Id.Should().Be(command.UserId);
            property.PropertyValues.First().Amount.Should().Be(command.PropertyValue);
        }

        [Fact]
        public async Task GetDashboardChartData_Should_Return_Correct_Assets_Over_1_year_period()
        {
            //Arrange
            var ownerId = new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF");
            var query = new GetDashboardChartData.Query(ownerId, false, 12);

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: query);

            var vehicles = await _fixture.VehicleRepository.GetVehiclesByOwnerId(CustomerId.Parse(ownerId));
            var properties = await _fixture.PropertyRepository.GetPropertiesByOwnerId(CustomerId.Parse(ownerId));
            var investments = await _fixture.InvestmentRepository.GetInvestmentsByOwnerId(CustomerId.Parse(ownerId));
            var calculatedResult = (vehicles.IsNotNullOrEmpty() ? vehicles.FirstOrDefault().VehicleValues.FirstOrDefault().Amount : 0) +
                                   (properties.IsNotNullOrEmpty() ? properties.FirstOrDefault().PropertyValues.FirstOrDefault().Amount : 0) +
                                   (investments.IsNotNullOrEmpty() ? investments.FirstOrDefault().InvestmentValues.FirstOrDefault().Amount : 0);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            calculatedResult.Should().Be(result.Records.FirstOrDefault().Data.Assets);
        }

        [Fact]
        public async Task DeleteUserRelatedAccounts_Should_Delete_User_Related_Data()
        {
            //Arrange
            var ownerId = new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF");

            var command = new DeleteUserRelatedData.Command(ownerId);

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            var ownerAccounts = await _fixture.AccountRepository.GetAccountsByOwnerId(CustomerId.Parse(ownerId));
            var ownerProperties = await _fixture.PropertyRepository.GetPropertiesByOwnerId(CustomerId.Parse(ownerId));
            var ownerRefreshHistories = await _fixture.RefreshHistoryRepository.GetRefreshHistoriesByUserId(ownerId);
            var ownerVaultItems = await _fixture.VaultItemRepository.GetOwnerVaultItems(CustomerId.Parse(ownerId));
            var ownerVehicles = await _fixture.VehicleRepository.GetVehiclesByOwnerId(CustomerId.Parse(ownerId));
            var personalItems = await _fixture.PersonalItemRepository.GetPersonalItemsByOwnerId(CustomerId.Parse(ownerId));
            var investments = await _fixture.InvestmentRepository.GetInvestmentsByOwnerId(CustomerId.Parse(ownerId));

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            ownerAccounts.Should().BeEmpty();
            ownerProperties.Should().BeEmpty();
            ownerRefreshHistories.Should().BeEmpty();
            ownerVaultItems.Should().BeEmpty();
            ownerVehicles.Should().BeEmpty();
            personalItems.Should().BeEmpty();
            investments.Should().BeEmpty();
        }
    }
}