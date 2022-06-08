using FluentAssertions;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.VaultService.UnitTest.ManualAccountDetails
{
    public class ManualAccountUpdatesTests : IClassFixture<ManualAccountDetailsFixture>
    {
        private readonly ManualAccountDetailsFixture _fixture;

        public ManualAccountUpdatesTests(ManualAccountDetailsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UpdateVehicleDetails_Invalid_Id_Should_Return_BadRequest()
        {
            var command = new UpdateVehicle.Command() { VehicleId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateVehicleDetails_Valid_Model_Should_Return_OK()
        {
            var command = new UpdateVehicle.Command
            {
                VehicleId = new Guid("c5682a34-819f-4123-94d4-2963ae350981"),
                VehicleName = "Vehicle",
                Registration = "ABC 123",
                Mileage = 7482,
                VehicleValue = 1900m,
                AutomaticallyReValueVehicle = false,
                Reference = "Ref No. AAAA",
                Notes = "This is some notes asad",
                FinanceAgreementName = "Financial Agreement SDASD",
                NewFinanceBalance = 20m,
                VehicleValueChanged = true,
                FinanceBalanceChanged = true,
            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var vehicle = await _fixture.VehicleRepository.GetVehicleDetails(command.VehicleId, CancellationToken.None);

            vehicle.VehicleName.Should().Be(command.VehicleName);
            vehicle.Registration.Should().Be(command.Registration);
            vehicle.Reference.Should().Be(command.Reference);
            vehicle.Notes.Should().Be(command.Notes);
            vehicle.FinanceAgreementName.Should().Be(command.FinanceAgreementName);
            vehicle.VehicleValue.Should().Be(command.VehicleValue);
            vehicle.NewFinanceBalance.Should().Be(command.NewFinanceBalance);
        }

        [Fact]
        public async Task UpdateVehicleDetails_Values_Not_Changed_Should_Be_No_New_Values()
        {
            var command = new UpdateVehicle.Command
            {
                VehicleId = new Guid("c5682a34-819f-4123-94d4-2963ae350981"),
                VehicleName = "Manual Account Test Vehicle",
                Registration = "ABC",
                Mileage = 7482,
                VehicleValue = 1900m,
                AutomaticallyReValueVehicle = false,
                Reference = "Ref No.",
                Notes = "This is some notes",
                FinanceAgreementName = "Financial Agreement",
                NewFinanceBalance = 20m,
                VehicleValueChanged = false,
                FinanceBalanceChanged = false,
            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var vehicle = await _fixture.VehicleRepository.GetVehicleDetails(command.VehicleId, CancellationToken.None);

            vehicle.VehicleName.Should().Be(command.VehicleName);
            vehicle.Registration.Should().Be(command.Registration);
            vehicle.Reference.Should().Be(command.Reference);
            vehicle.Notes.Should().Be(command.Notes);
            vehicle.FinanceAgreementName.Should().Be(command.FinanceAgreementName);
            vehicle.VehicleValue.Should().NotBe(command.VehicleValue);
            vehicle.NewFinanceBalance.Should().NotBe(command.NewFinanceBalance);
        }

        [Fact]
        public async Task UpdateVehicleDetails_Should_Throw_Bad_Request_If_Vehicle_Name_Exists()
        {
            // Arrange
            var command = new UpdateVehicle.Command
            {
                VehicleId = Guid.NewGuid(),
                VehicleName = "Manual Account Test Vehicle",
                Registration = "ABC",
                Mileage = 7482,
                VehicleValue = 1900m,
            };
            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);
            // Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePropertyDetails_Invalid_Id_Should_Return_BadRequest()
        {
            var command = new UpdateProperty.Command() { PropertyId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePropertyDetails_Valid_Model_Should_Return_OK()
        {
            var command = new UpdateProperty.Command
            {
                PropertyName = "New Name",
                NumberOfBedrooms = 21,
                Postcode = "Postcode",
                PropertyValue = 1230m,
                PropertyValueChange = true,
                AutomaticallyReValueProperty = false,
                Address = "New Address",
                FinanceAgreementName = "Provider",
                Reference = "Reference",
                NewFinanceBalance = 12330m,
                MortgageBalanceChange = true,
                Notes = "Notes",
                PropertyId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e"),
            };

            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var property = await _fixture.PropertyRepository.GetPropertyDetails(command.PropertyId, CancellationToken.None);

            property.PropertyName.Should().Be(command.PropertyName);
            property.NumberOfBedrooms.Should().Be(command.NumberOfBedrooms);
            property.Notes.Should().Be(command.Notes);
            property.NewFinanceBalance.Should().Be(command.NewFinanceBalance);
            property.PropertyValue.Should().Be(command.PropertyValue);
            property.Address.Should().Be(command.Address);
            property.AutomaticallyReValueProperty.Should().Be(command.AutomaticallyReValueProperty);
            property.FinanceAgreementName.Should().Be(command.FinanceAgreementName);
            property.Postcode.Should().Be(command.Postcode);
            property.Reference.Should().Be(command.Reference);
        }

        [Fact]
        public async Task UpdatePropertyDetails_Values_Not_Changed_Should_Be_No_New_Values()
        {
            var command = new UpdateProperty.Command
            {
                PropertyName = "New Name",
                NumberOfBedrooms = 21,
                Postcode = "Postcode",
                PropertyValue = 123022m,
                PropertyValueChange = false,
                AutomaticallyReValueProperty = false,
                Address = "New Address",
                FinanceAgreementName = "Provider",
                Reference = "Reference",
                NewFinanceBalance = 1233220m,
                MortgageBalanceChange = false,
                Notes = "Notes",
                PropertyId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e"),
            };

            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var property =
                await _fixture.PropertyRepository.GetPropertyDetails(command.PropertyId, CancellationToken.None);

            property.NewFinanceBalance.Should().NotBe(command.NewFinanceBalance);
            property.PropertyValue.Should().NotBe(command.PropertyValue);
        }

        [Fact]
        public async Task UpdatePropertyDetails_Should_Throw_Bad_Request_If_Property_Name_Exists()
        {
            // Arrange
            var command = new UpdateProperty.Command
            {
                PropertyName = "New Name",
                NumberOfBedrooms = 21,
                Postcode = "Postcode",
                PropertyValue = 123022m,
                Address = "New Address",
                NewFinanceBalance = 1233220m,
                PropertyId = Guid.NewGuid()
            };
            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);
            // Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePersonalItemDetails_Invalid_Id_Should_Return_BadRequest()
        {
            var command = new UpdatePersonalItem.Command() { PersonalItemId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePersonalItemDetails_Valid_Model_Should_Return_OK()
        {
            var command = new UpdatePersonalItem.Command
            {
                ItemName = "New Name",
                Value = 1200m,
                FinanceAgreementName = "Finance New Name",
                NewFinanceBalance = 125m,
                Reference = "New Ref",
                Notes = "New Notes",
                PersonalItemId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"),
                PersonalItemValueChange = true,
                FinanceBalanceChange = true,

            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var item = await _fixture.PersonalItemRepository.GetPersonalItemDetails(command.PersonalItemId,
                CancellationToken.None);

            item.Notes.Should().Be(command.Notes);
            item.Reference.Should().Be(command.Reference);
            item.ItemName.Should().Be(command.ItemName);
            item.Value.Should().Be(command.Value);
            item.FinanceAgreementName.Should().Be(command.FinanceAgreementName);
            item.NewFinanceBalance.Should().Be(command.NewFinanceBalance);
        }

        [Fact]
        public async Task UpdatePersonalItemDetails_Values_Not_Changed_Should_Be_No_New_Values()
        {
            var command = new UpdatePersonalItem.Command
            {
                ItemName = "New Name",
                Value = 451200m,
                FinanceAgreementName = "Finance New Name",
                NewFinanceBalance = 12500m,
                Reference = "New Ref",
                Notes = "New Notes",
                PersonalItemId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5"),
                PersonalItemValueChange = false,
                FinanceBalanceChange = false,

            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var item = await _fixture.PersonalItemRepository.GetPersonalItemDetails(command.PersonalItemId,
                CancellationToken.None);

            item.NewFinanceBalance.Should().NotBe(command.NewFinanceBalance);
            item.Value.Should().NotBe(command.Value);
        }

        [Fact]
        public async Task UpdatePersonalItemDetails_Should_Throw_Bad_Request_If_Item_Name_Exists()
        {
            // Arrange
            var command = new UpdatePersonalItem.Command
            {
                ItemName = "New Name",
                Value = 451200m,
                PersonalItemId = Guid.NewGuid()
            };
            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);
            // Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateInvestmentDetails_Invalid_Id_Should_Return_BadRequest()
        {
            var command = new UpdateInvestment.Command() { InvestmentId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateInvestmentDetails_Valid_Model_Should_Return_OK()
        {
            var command = new UpdateInvestment.Command
            {
                InvestmentName = "New Name",
                Category = "New Category",
                TickerCode = "New Code",
                UnitPrice = 1232,
                Quantity = 1231,
                TotalValue = 2334,
                AutomaticallyRevalueInvestment = false,
                InvestmentId = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95"),
                InvestmentValueChanged = true,

            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var investment = await _fixture.InvestmentRepository.GetInvestmentDetails(command.InvestmentId, CancellationToken.None);

            investment.Category.Should().Be(command.Category);
            investment.TickerCode.Should().Be(command.TickerCode);
            investment.UnitPrice.Should().Be(command.UnitPrice);
            investment.Quantity.Should().Be(command.Quantity);
            investment.TotalValue.Should().Be(command.TotalValue);
            investment.AutomaticallyRevalueInvestment.Should().Be(command.AutomaticallyRevalueInvestment);
        }

        [Fact]
        public async Task UpdateInvestmentDetails_Values_Not_Changed_Should_Be_No_New_Values()
        {
            var command = new UpdateInvestment.Command
            {
                InvestmentName = "New Name",
                Category = "New Category",
                TickerCode = "New Code",
                UnitPrice = 1232,
                Quantity = 1231,
                TotalValue = 232234,
                AutomaticallyRevalueInvestment = false,
                InvestmentId = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95"),
                InvestmentValueChanged = false,

            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var investment = await _fixture.InvestmentRepository.GetInvestmentDetails(command.InvestmentId, CancellationToken.None);
        }

        [Fact]
        public async Task UpdateInvestmentDetails_Should_Throw_Bad_Request_If_Investment_Name_Exists()
        {
            // Arrange
            var command = new UpdateInvestment.Command
            {
                InvestmentName = "New Name",
                UnitPrice = 1232,
                Quantity = 1231,
                TotalValue = 232234,
                InvestmentId = Guid.NewGuid(),
            };
            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);
            // Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAccountDetails_Invalid_Id_Should_Return_BadRequest()
        {
            var command = new UpdateManualAccount.Command() { AccountId = Guid.NewGuid() };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAccountDetails_Valid_Model_Should_Return_Ok()
        {
            var command = new UpdateManualAccount.Command
            {
                AccountId = new Guid("3087c40e-4281-4636-bfed-fc1d3a499a26"),
                Balance = 1312m,
                AccountBalanceChanged = true,
                Type = 5,
                Name = "New Name",
                Notes = "New Notes",
                ReferenceNumber = "New Ref",
            };

            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var account = await _fixture.AccountRepository.GetManualAccount(command.AccountId, CancellationToken.None);

            account.ReferenceNumber.Should().Be(command.ReferenceNumber);
            account.Balance.Should().Be(command.Balance);
            account.Type.Should().Be(command.Type);
            account.Name.Should().Be(command.Name);
            account.Notes.Should().Be(command.Notes);
        }

        [Fact]
        public async Task UpdateAccountDetails_Values_Not_Changed_Should_Be_No_New_Values()
        {
            var command = new UpdateManualAccount.Command
            {
                AccountId = new Guid("3087c40e-4281-4636-bfed-fc1d3a499a26"),
                Balance = 1456m,
                AccountBalanceChanged = false,
                Type = 4,
                Name = "New Name",
                Notes = "New Notes",
                ReferenceNumber = "New Ref",

            };
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var account = await _fixture.AccountRepository.GetManualAccount(command.AccountId, CancellationToken.None);

            account.Balance.Should().NotBe(command.Balance);
        }

        [Fact]
        public async Task UpdateAccountDetails_Should_Throw_Bad_Request_If_Account_Name_Exists()
        {
            // Arrange
            var command = new UpdateManualAccount.Command
            {
                AccountId = Guid.NewGuid(),
                Balance = 1456m,
                Type = 4,
                Name = "New Name",
            };
            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);
            // Assert
            result.HttpStatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }
    }
}