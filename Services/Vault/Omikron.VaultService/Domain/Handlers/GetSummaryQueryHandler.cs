using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetSummaryQueryHandler : BaseHandlerLight<GetSummary.Query, ApiResult<GetSummaryViewModel>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;

        public GetSummaryQueryHandler(IAccountRepository accountRepository, IAccountService accountService)
        {
            _accountRepository = accountRepository;
            _accountService = accountService;
        }

        //Implementation of this handler may have to be changed once we have real data
        public override async Task<ApiResult<GetSummaryViewModel>> Handle(GetSummary.Query request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAccountsWithBalanceByOwnerId(request.UserId, cancellationToken);

            if (accounts.IsNullOrEmpty())
            {
                return ApiResult<GetSummaryViewModel>.BadRequest("User does not have any accounts");
            }

            var result = _accountService.GetTotalAssetsAndLiabilities(accounts);

            return ApiResult<GetSummaryViewModel>.Success().WithData(result);
        }
    }
}
