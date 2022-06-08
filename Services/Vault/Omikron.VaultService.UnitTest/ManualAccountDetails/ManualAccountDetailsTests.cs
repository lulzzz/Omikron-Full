using FluentAssertions;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Investment;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Vehicle;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ValueValueObject = Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Vehicle.ValueValueObject;

namespace Omikron.VaultService.UnitTest.ManualAccountDetails
{
    public class ManualAccountDetailsTests : IClassFixture<ManualAccountDetailsFixture>
    {
        private readonly ManualAccountDetailsFixture _fixture;

        public ManualAccountDetailsTests(ManualAccountDetailsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Vehicles_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Vehicle, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Vehicles_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Manual Account Test Vehicle",
                FinanceName = "Financial Agreement",
                FinanceType = AssetType.VehicleFinance,
                FinanceId = new Guid("5c28fd95-aa61-4422-b514-544faa1bb1a6"),
                AccountId = new Guid("c5682a34-819f-4123-94d4-2963ae350981"),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                Details = new ()
                {
                    new MileageValueObject("7482"){RenderCurrency = false, Name = "Mileage"},
                    new RegistrationValueObject("ABC"){RenderCurrency = false, Name = "Registration"},
                    new ValueValueObject(new Vehicle()){ItemValue = 2000m, RenderCurrency = true, Name = "Value"},
                    new FinanceBalanceValueObject(null){ ItemValue = 100m, RenderCurrency = true, Name = "Finance Balance"},
                    new ReferenceValueObject(null){ ItemValue = "Ref No.", RenderCurrency = false, Name = "Reference"},
                },
                Transactions = new List<VaultItemValueViewModel>
                {
                    new() {Amount = 2200m, Currency = "GBP", Type = "Value Change"},
                    new() {Amount = 2000m, Currency = "GBP", Type = "Value Change"},
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Vehicle, AccountId = new Guid("c5682a34-819f-4123-94d4-2963ae350981") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();
            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Vehicles_Vehicle_Has_No_Finance_Should_Return_Ok()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Vehicle, AccountId = new Guid("f5682a34-819f-4123-94d4-2963ae350982") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Vehicle_finance_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.VehicleFinance, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Vehicle_Finance_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Financial Agreement",
                FinanceName = "Manual Account Test Vehicle",
                FinanceType = AssetType.Vehicle,
                TotalBalance = -100m,
                FinanceId = new Guid("5c28fd95-aa61-4422-b514-544faa1bb1a6"),
                AccountId = new Guid("c5682a34-819f-4123-94d4-2963ae350981"),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                CreditDebitIndicator = CreditDebitIndicator.Debit,
                Transactions = new List<VaultItemValueViewModel>
                {
                    new (){Amount = 200m, Type = "Value Change", Currency = "GBP", Date = new DateTime(2021, 3,1)},
                    new (){Amount = 100m, Type = "Value Change", Currency = "GBP", Date = new DateTime(2021, 3,2)},
                },
                Details = new()
                {
                    new ReferenceValueObject(new Account { Name = "Ref No", ReferenceNumber = "Ref No." })
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.VehicleFinance, AccountId = new Guid("c5682a34-819f-4123-94d4-2963ae350981"), FinanceId = new Guid("5c28fd95-aa61-4422-b514-544faa1bb1a6") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Property_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Property, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Property_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Manual Account Test Property",
                FinanceName = "Mortgage",
                FinanceType = AssetType.Mortgage,
                FinanceId = new Guid("e1a2ac29-0f0c-49d0-9866-3b2344ae0d1b"),
                AccountId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e"),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                Details = new ()
                {
                    new AddressValueObject("This is the property address"){RenderCurrency = false, Name = "Address"},
                    new SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property.ValueValueObject(new Property()){ ItemValue = 11000m, RenderCurrency = true, Name = "Value"},
                    new MortgageBalanceValueObject(null){ ItemValue = 100m, RenderCurrency = true, Name = "Mortgage Balance"},
                    new BedroomsValueObject(2){RenderCurrency = false, Name = "Bedrooms"},
                    new ReferenceValueObject(new Account { ReferenceNumber = "Ref No."})
                },
                Transactions = new List<VaultItemValueViewModel>
                {
                    new() {Amount = 123000m, Currency = "GBP", Type = "Value Change"},
                    new() {Amount = 11000m, Currency = "GBP", Type = "Value Change"},
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Property, AccountId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Mortgage_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Mortgage, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemTypeIs_Mortgage_Finance_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Mortgage",
                FinanceName = "Manual Account Test Property",
                FinanceType = AssetType.Property,
                TotalBalance = -100m,
                FinanceId = new Guid("e1a2ac29-0f0c-49d0-9866-3b2344ae0d1b"),
                AccountId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e"),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                CreditDebitIndicator = CreditDebitIndicator.Debit,
                Transactions = new List<VaultItemValueViewModel>
                {
                    new (){Amount = 200m, Type = "Value Change", Currency = "GBP",Date =  new DateTime(2021, 3,1)},
                    new (){Amount = 100m, Type = "Value Change", Currency = "GBP",Date = new DateTime(2021, 3,2)},
                },
                Details = new()
                {
                    new ReferenceValueObject(new Account { Name = "Ref No", ReferenceNumber = "Ref No." })
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Mortgage, AccountId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e"), FinanceId = new Guid("e1a2ac29-0f0c-49d0-9866-3b2344ae0d1b") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Investment_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Investment, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_Investment_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Test",
                TotalBalance = 230,
                AccountId = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95"),
                CurrencyCode = "Dollar",
                Details = new()
                {
                    new CategoryValueObject("Investment") { RenderCurrency = false, Name = "Category" },
                    new TickerCodeValueObject("Code") { RenderCurrency = false, Name = "Ticker Code" },
                    new UnitPriceValueObject(10m) { RenderCurrency = true, Name = "Unit Price" },
                    new QuantityValueObject(1000) { RenderCurrency = false, Name = "Quantity" },
                },
                Transactions = new List<VaultItemValueViewModel>
                {
                    new (){Amount = 200m, Type = "Value Change", Currency = "GBP",Date =  new DateTime(2021, 2,1)},
                    new (){Amount = 100m, Type = "Value Change", Currency = "GBP",Date = new DateTime(2021, 2,3)},
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Investment, AccountId = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_PersonalItem_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.PersonalItem, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_PersonalItem_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Personal Item",
                FinanceName = "Financial Agreement",
                FinanceType = AssetType.PersonalItemFinance,
                FinanceId = new Guid("1d9c3b59-3c98-4ea2-b31a-4960e227524d"),
                AccountId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                Details = new()
                {
                    new FinanceBalanceValueObject(null) { ItemValue = 100m, RenderCurrency = true, Name = "Finance Balance" },
                    new ReferenceValueObject(null) { ItemValue = "Ref No.", RenderCurrency = false, Name = "Reference" },
                },
                Transactions = new List<VaultItemValueViewModel>
                {
                 new()   { Amount = 2200.0M, Currency = "GBP", Type = "Value Change" },
                 new()   { Amount = 2000.0M, Currency = "GBP", Type = "Value Change" },
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.PersonalItem, AccountId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_PersonalItemFinance_Id_Is_Invalid_Should_Return_NotFound()
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.PersonalItemFinance, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ManualAccountDetails_ItemType_Is_PersonalItemFinance_Id_Is_Valid_Should_Return_Populated_ViewModel()
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = "Financial Agreement",
                FinanceName = "Personal Item",
                FinanceType = AssetType.PersonalItem,
                TotalBalance = -100m,
                FinanceId = new Guid("1d9c3b59-3c98-4ea2-b31a-4960e227524d"),
                AccountId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                CreditDebitIndicator = CreditDebitIndicator.Debit,
                Transactions = new List<VaultItemValueViewModel>
                {
                    new() { Amount = 200m, Type = "Value Change", Currency = "GBP", Date = new DateTime(2021, 3, 1) },
                    new() { Amount = 100m, Type = "Value Change", Currency = "GBP", Date = new DateTime(2021, 3, 2) },
                },
                Details = new ()
                {
                     new ReferenceValueObject(new Account { Name = "Ref No", ReferenceNumber = "Ref No." })
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.PersonalItemFinance, AccountId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"), FinanceId = new Guid("1d9c3b59-3c98-4ea2-b31a-4960e227524d") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("CurrentAccount")]
        [InlineData("SavingsAccount")]
        [InlineData("CreditCard")]
        [InlineData("Loan")]
        [InlineData("Pension")]
        public async Task ManualAccountDetails_ManualAccounts_Id_Is_Invalid_Should_Return_NotFound(string assetType)
        {
            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Parse(assetType), AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("3087c40e-4281-4636-bfed-fc1d3a499a26", "CurrentAccount")]
        [InlineData("b59df78a-5db7-4a2a-b5b4-7eb4573be066", "SavingsAccount")]
        [InlineData("e7997e74-13e7-4ee1-8788-edd7dc19737b", "CreditCard")]
        [InlineData("1d371241-00b5-4cc9-8bed-5d2e2cf4a3d2", "Loan")]
        [InlineData("2a999a77-d28f-4826-a384-88baf3de502a", "Pension")]
        public async Task ManualAccountDetails_ManualAccounts_Id_Is_Valid_Should_Return_Populated_ViewModel(string itemId, string assetType)
        {
            var expectedResult = new ManualAccountDetailsViewModel
            {
                Name = assetType,
                TotalBalance = -100m,
                FinanceId = new Guid(itemId),
                CurrencyCode = "GBP",
                Notes = "This is some notes",
                CreditDebitIndicator = CreditDebitIndicator.Debit,
                Transactions = new List<VaultItemValueViewModel>
                {
                    new VaultItemValueViewModel {Amount = 200M, Currency = "GBP", Type = "Value Change", Date = new DateTime(2021, 3,1)},
                    new VaultItemValueViewModel {Amount = 100M, Currency = "GBP", Type = "Value Change", Date = new DateTime(2021, 3,2)},
                },
                Details = new()
                {
                    new ReferenceValueObject(new Account { Name = "Ref No", ReferenceNumber = "Ref No." })
                }
            };

            var query = new GetManualAccountDetails.Query { ItemType = AssetType.Parse(assetType), AccountId = new Guid(itemId) };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().NotBeNull();

            result.Records.Should().BeEquivalentTo(expectedResult);
        }
    }
}