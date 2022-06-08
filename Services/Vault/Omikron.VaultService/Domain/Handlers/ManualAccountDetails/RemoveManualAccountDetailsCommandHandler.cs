using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemoveManualAccountDetailsCommandHandler : BaseHandler<RemoveManualAccount.Command, ApiResult>
    {
        private readonly Dictionary<AssetType, Func<Guid, bool, BaseCommand.Action<bool>>> _assetCommands;

        public RemoveManualAccountDetailsCommandHandler(IDispatcher dispatcher) : base(dispatcher)
        {
            _assetCommands = new Dictionary<AssetType, Func<Guid, bool, BaseCommand.Action<bool>>>()
            {
                {AssetType.Mortgage, (id, isArchive) => new RemoveMortgage.Command() {AccountId = id, IsArchived = isArchive}},
                {AssetType.Property, (id, isArchive) => new RemoveProperty.Command() {AccountId = id, IsArchived = isArchive}},
                {AssetType.Vehicle, (id, isArchive) => new RemoveVehicle.Command() {AccountId = id, IsArchived = isArchive}},
                {AssetType.VehicleFinance, (id, isArchive) => new RemoveVehicleFinance.Command(){AccountId = id, IsArchived = isArchive}},
                {AssetType.PersonalItem, (id, isArchive) => new RemovePersonalItem.Command() { AccountId = id, IsArchived = isArchive } },
                {AssetType.PersonalItemFinance, (id, isArchive) => new RemovePersonalItemFinance.Command() { AccountId = id, IsArchived = isArchive} },
                {AssetType.Investment, (id, isArchive) => new RemoveInvestment.Command() { AccountId = id, IsArchived = isArchive } },
                {AssetType.Account, (id, isArchive) => new RemoveAccount.Command() { AccountId = id, IsArchived = isArchive, AccountType = AccountType.Pensions } },
            };
        }
        public override async Task<ApiResult> Handle(RemoveManualAccount.Command request, CancellationToken cancellationToken)
        {
            if (!_assetCommands.ContainsKey(request.AccountType))
            {
                throw new NotImplementedException();
            }

            var result = await Dispatcher.DispatchAsync(_assetCommands[request.AccountType].Invoke(request.AccountId, request.IsArchived),cancellationToken);

            return result ? ApiResult.Success() : ApiResult.BadRequest();
        }
    }
}