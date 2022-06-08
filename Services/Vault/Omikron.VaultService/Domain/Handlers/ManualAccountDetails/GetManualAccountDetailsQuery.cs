using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetManualAccountDetailsQuery : BaseHandler<GetManualAccountDetails.Query, ApiResult<ManualAccountDetailsViewModel>>
    {
        private readonly Dictionary<AssetType, Func<Guid, Guid, BaseCommand.Action<ManualAccountDetailsViewModel>>> _assetQueries;

        public GetManualAccountDetailsQuery(IDispatcher dispatcher) : base(dispatcher)
        {
            _assetQueries = new()
            {
                { AssetType.Mortgage, (id, financeId) => new GetMortgageDetails.Query() { AccountId = id, FinanceId = financeId } },
                { AssetType.Property, (id, _) => new Queries.ManualAccountDetails.GetPropertyDetails.Query() { AccountId = id } },
                { AssetType.Vehicle, (id, _) => new Queries.ManualAccountDetails.GetVehicleDetails.Query() { AccountId = id } },
                { AssetType.VehicleFinance, (id, financeId) => new GetVehicleFinanceDetails.Query() { AccountId = id, FinanceId = financeId } },
                { AssetType.PersonalItem, (id, _) => new Queries.ManualAccountDetails.GetPersonalItemDetails.Query { AccountId = id } },
                { AssetType.PersonalItemFinance, (id, financeId) => new GetPersonalItemFinanceDetails.Query() { AccountId = id, FinanceId = financeId } },
                { AssetType.Investment, (id, _) => new Queries.ManualAccountDetails.GetInvestmentDetails.Query() { AccountId = id } },
                { AssetType.Account, (id, _) => new Queries.ManualAccountDetails.GetAccountDetails.Query() { AccountId = id, AccountType = AccountType.Pensions } },
            };
        }

        public override async Task<ApiResult<ManualAccountDetailsViewModel>> Handle(GetManualAccountDetails.Query request, CancellationToken cancellationToken)
        {
            if (!_assetQueries.ContainsKey(request.ItemType))
            {
                throw new NotImplementedException();
            }

            var query = _assetQueries[request.ItemType];

            var viewModel = await Dispatcher.DispatchAsync(query.Invoke(request.AccountId, request.FinanceId), cancellationToken);

            if (viewModel == null)
            {
                return ApiResult<ManualAccountDetailsViewModel>.NotFound();
            }

            if (string.IsNullOrWhiteSpace(viewModel.CurrencyCode))
            {
                viewModel.CurrencyCode = Constants.DefaultCurrencyCode;
            }

            if(viewModel.CreditDebitIndicator.IsNotNull() && viewModel.CreditDebitIndicator == CreditDebitIndicator.Debit)
            {
                viewModel.TotalBalance *= (-1);
            }
            
            return ApiResult<ManualAccountDetailsViewModel>.Success().WithData(viewModel);
        }
    }
}