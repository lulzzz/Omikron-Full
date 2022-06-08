using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetManualAccountQueryHandler : BaseHandlerLight<GetManualAccount.Query, ApiResult<ManualAccountViewModel>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPersonalItemRepository _personalItemRepository;

        public GetManualAccountQueryHandler(IAccountRepository accountRepository, IVehicleRepository vehicleRepository, 
            IPropertyRepository propertyRepository, IPersonalItemRepository personalItemRepository)
        {
            _accountRepository = accountRepository;
            _vehicleRepository = vehicleRepository;
            _propertyRepository = propertyRepository;
            _personalItemRepository = personalItemRepository;
        }

        public override async Task<ApiResult<ManualAccountViewModel>> Handle(GetManualAccount.Query request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetManualAccount(request.AccountId, cancellationToken);

            if(account.IsNull())
            {
                return ApiResult<ManualAccountViewModel>.NotFound();
            }

            if(account.LoanType.IsNotNull())
            {
                account.AssetId = account.LoanType == LoanType.Mortgage.Name ? _propertyRepository.GetPropertyIdByMortgageId(request.AccountId)
                                                                        : _vehicleRepository.GetVehicleIdByFinanceAgreementId(request.AccountId);

                account.AssetType = account.LoanType == LoanType.Mortgage.Name ? AssetType.Mortgage.Name : AssetType.VehicleFinance.Name;

                if (account.AssetId.IsNull() && account.LoanType == LoanType.FinancialAgreement.Name)
                {
                    account.AssetId = _personalItemRepository.GetPersonalItemIdByFinanceAgreementId(request.AccountId);
                    account.AssetType = AssetType.PersonalItemFinance.Name;

                    if (account.AssetId.IsNullOrEmpty())
                    {
                        account.LoanType = null;
                        await _accountRepository.SaveAsync(cancellationToken);
                        return ApiResult<ManualAccountViewModel>.Success().WithData(account);
                    }
                }
            }
          
            return ApiResult<ManualAccountViewModel>.Success().WithData(account);
        }
    }
}