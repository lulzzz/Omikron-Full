using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetAvailableLoansQueryHandler : BaseHandlerLight<GetAvailableLoans.Query, ApiResult<IEnumerable<LoanViewModel>>>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IAccountService _accountService;

		public GetAvailableLoansQueryHandler(IAccountRepository accountRepository, IAccountService accountService)
		{
			_accountRepository = accountRepository;
			_accountService = accountService;
		}

		public override async Task<ApiResult<IEnumerable<LoanViewModel>>> Handle(GetAvailableLoans.Query request, CancellationToken cancellationToken)
		{
			var loans = await _accountRepository.SearchLoans(CustomerId.Parse(request.UserId), request.Search, cancellationToken);
			var result = loans.Select(a =>
			{
				var lastBalance = _accountService.GetLastBalance(a);
				var lastBalanceAmount = lastBalance != null ? lastBalance.Amount : 0;
				var lastBalanceCreditDebitIndicator = lastBalance != null ? lastBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;

				return new LoanViewModel(a.ExternalId, a.Name, lastBalanceCreditDebitIndicator == CreditDebitIndicator.Credit ? lastBalanceAmount : lastBalanceAmount * (-1));

			});

			return ApiResult<IEnumerable<LoanViewModel>>.Success().WithData(result);
		}
	}
}
