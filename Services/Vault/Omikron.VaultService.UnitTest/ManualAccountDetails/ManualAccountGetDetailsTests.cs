using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Commands;
using Omikron.VaultService.Domain.Queries;
using Xunit;

namespace Omikron.VaultService.UnitTest.ManualAccountDetails
{
    public class ManualAccountGetDetailsTests : IClassFixture<ManualAccountDetailsFixture>
    {
        private readonly ManualAccountDetailsFixture _fixture;

        public ManualAccountGetDetailsTests(ManualAccountDetailsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetVehicleDetails_Model_Is_Null_Should_Return_NotFound()
        {
            var query = new GetVehicleDetails.Query() { AccountId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetVehicleDetails_Model_Is_Not_Null_Should_Return_Ok_And_Model()
        {
            var expectedResult = new VehicleViewModel
            {
                VehicleId = new Guid("c5682a34-819f-4123-94d4-2963ae350981"),
                VehicleName = "Manual Account Test Vehicle",
                Registration = "ABC",
                Mileage = 7482,
                VehicleValue = 2000m,
                AutomaticallyReValueVehicle = false,
                Reference = "Ref No.",
                Notes = "This is some notes",
                FinanceAgreementName = "Financial Agreement",
                NewFinanceBalance = 100m
            };

            var query = new GetVehicleDetails.Query() { AccountId = new Guid("c5682a34-819f-4123-94d4-2963ae350981") };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetVehicileDetails_Vehicle_Has_No_Finance_Should_Return_Ok()
        {
            var query = new GetVehicleDetails.Query() { AccountId = new Guid("f5682a34-819f-4123-94d4-2963ae350982") };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPropertyDetails_Model_Is_Null_Should_Return_NotFound()
        {
            var query = new GetPropertyDetails.Query() { AccountId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetPropertyDetails_Model_Is_Not_Null_Should_Return_Ok_And_Model()
        {
            var expectedResult = new PropertyViewModel
            {
                PropertyName = "Manual Account Test Property",
                NumberOfBedrooms = 2,
                Postcode = "ABC1 1ZY",
                PropertyValue = 11000m,
                AutomaticallyReValueProperty = false,
                Address = "This is the property address",
                FinanceAgreementName = "Mortgage",
                Reference = "Ref No.",
                Notes = "This is some notes",
                NewFinanceBalance = 100m,
                PropertyId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e")
            };

            var query = new GetPropertyDetails.Query() { AccountId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e") };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetPersonalItemDetails_Model_Is_Null_Should_Return_NotFound()
        {
            var query = new GetPersonalItemDetails.Query() { AccountId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetPersonalItemDetails_Model_Is_Not_Null_Should_Return_Ok_And_Model()
        {
            var expectedResult = new PersonalItemViewModel
            {
                ItemName = "Personal Item",
                Value = 2000m,
                Reference = "Ref No.",
                Notes = "This is some notes",
                FinanceAgreementName = "Financial Agreement",
                NewFinanceBalance = 100m
            };

            var query = new GetPersonalItemDetails.Query() { AccountId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5") };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetInvestmentDetails_Model_Is_Null_Should_Return_NotFound()
        {
            var query = new GetInvestmentDetails.Query() { AccountId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetInvestmentDetails_Model_Is_Not_Null_Should_Return_Ok_And_Model()
        {
            var expectedResult = new InvestmentViewModel
            {
                Category = "Investment",
                InvestmentName = "Test",
                TickerCode = "Code",
                UnitPrice = 10,
                Quantity = 1000,
                TotalValue = 230,
                AutomaticallyRevalueInvestment = true
            };

            var query = new GetInvestmentDetails.Query() { AccountId = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95") };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetManualAccount_Model_Is_Null_Should_Return_NotFound()
        {
            var query = new GetManualAccount.Query() { AccountId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetManualAccount_Model_Is_Not_Null_Should_Return_Ok_And_Model()
        {
            var expectedResult = new ManualAccountViewModel
            {
                Type = AccountType.Loan.Id,
                Name = "CurrentAccount",
                Balance = 100m,
                Notes = "This is some notes",
                OwnerId = "43FE198B-1435-483B-80E8-3B0A2BBDAEDF",
                ReferenceNumber = "Ref No.",
            };

            var query = new GetManualAccount.Query() { AccountId = new Guid("3087c40e-4281-4636-bfed-fc1d3a499a26") };
            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Should().BeEquivalentTo(expectedResult);
        }
    }
}