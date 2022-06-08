using FluentAssertions;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.VaultService.UnitTest.ManualAccountDetails
{
    public class RemoveManualAccountTests : IClassFixture<ManualAccountDetailsFixture>
    {
        private readonly ManualAccountDetailsFixture _fixture;

        public RemoveManualAccountTests(ManualAccountDetailsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Vehicles_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Vehicle, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Vehicles_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Vehicle, AccountId = new Guid("c5682a34-819f-4123-94d4-2963ae350981") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Vehicle_finance_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.VehicleFinance, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Vehicle_Finance_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.VehicleFinance, AccountId = new Guid("5c28fd95-aa61-4422-b514-544faa1bb1a6") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK); }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Property_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Property, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Property_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Property, AccountId = new Guid("e6350213-7ada-4dea-97eb-781a61202f4e") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK); }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Mortgage_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Mortgage, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountTypeIs_Mortgage_Finance_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Mortgage, AccountId = new Guid("e1a2ac29-0f0c-49d0-9866-3b2344ae0d1b") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Investment_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Investment, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_Investment_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Investment, AccountId = new Guid("0dd652f4-dfb7-4165-a7a7-a6d748789c95") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_PersonalItem_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.PersonalItem, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_PersonalItem_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.PersonalItem, AccountId = new Guid("731b9849-0f28-45e0-b485-c012a8903cb5") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_PersonalItemFinance_Id_Is_Invalid_Should_Return_BadRequest()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.PersonalItemFinance, AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ManualAccountDetails_AccountType_Is_PersonalItemFinance_Id_Is_Valid_Should_Return_200()
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.PersonalItemFinance, AccountId = new Guid("1d9c3b59-3c98-4ea2-b31a-4960e227524d") };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("CurrentAccount")]
        [InlineData("SavingsAccount")]
        [InlineData("CreditCard")]
        [InlineData("Loan")]
        [InlineData("Pension")]
        public async Task ManualAccountDetails_ManualAccounts_Id_Is_Invalid_Should_Return_BadRequest(string assetType)
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Parse(assetType), AccountId = Guid.NewGuid() };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("3087c40e-4281-4636-bfed-fc1d3a499a26", "CurrentAccount")]
        [InlineData("b59df78a-5db7-4a2a-b5b4-7eb4573be066", "SavingsAccount")]
        [InlineData("e7997e74-13e7-4ee1-8788-edd7dc19737b", "CreditCard")]
        [InlineData("1d371241-00b5-4cc9-8bed-5d2e2cf4a3d2", "Loan")]
        [InlineData("2a999a77-d28f-4826-a384-88baf3de502a", "Pension")]
        public async Task ManualAccountDetails_ManualAccounts_Id_Is_Valid_Should_Return_200(string itemId, string assetType)
        {
            var query = new RemoveManualAccount.Command() { AccountType = AssetType.Parse(assetType), AccountId = new Guid(itemId) };

            var result = await _fixture.Dispatcher.DispatchAsync(query);

            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}