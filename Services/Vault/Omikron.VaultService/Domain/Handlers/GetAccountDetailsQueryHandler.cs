using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.VaultService.Domain.Queries;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Configuration;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetAccountDetailsQueryHandler : BaseHandlerLight<GetAccountDetails.Query, ApiResult<AccountViewModel>>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;
		private readonly BudProviderIcons _budProviderIcons;

		public GetAccountDetailsQueryHandler(IAccountRepository accountRepository, IAccountService accountService, IMapper mapper, BudProviderIcons budProviderIcons)
		{
			_accountRepository = accountRepository;
			_accountService = accountService;
			_mapper = mapper;
			_budProviderIcons = budProviderIcons;
		}

		public override async Task<ApiResult<AccountViewModel>> Handle(GetAccountDetails.Query request, CancellationToken cancellationToken)
		{
			var account = await _accountRepository.GetUserAccountAsync(request.AccountId, cancellationToken);

			if (account == null)
			{
				return ApiResult<AccountViewModel>.BadRequest("Account does not exist.");
			}

			var result = _mapper.Map<Account, AccountViewModel>(account);
			var lastBalance = _accountService.GetLastBalance(account);
			var lastBalanceAmount = lastBalance != null ? lastBalance.Amount : 0;
			var lastBalanceCreditDebitIndicator = lastBalance != null ? lastBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;

			result.Value = lastBalanceCreditDebitIndicator == CreditDebitIndicator.Credit ? lastBalanceAmount : lastBalanceAmount * (-1);
			result.ImageUrl = _budProviderIcons.Providers.TryGetValue(result.Provider ?? "", out var providerIcon) ? providerIcon.Icon : "";

			return ApiResult<AccountViewModel>.Success().WithData(result);
		}
	}
}