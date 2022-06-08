using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using Omikron.Sync.Bud.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Service.Business.Handlers
{
	public class InitiateRefreshCommandHandler : BaseHandlerLight<InitiateRefresh.Command, ApiResult>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IRefreshHistoryRepository _refreshHistoryRepository;
		private readonly IBudApiService _budApiService;

		public InitiateRefreshCommandHandler(IAccountRepository accountRepository, IRefreshHistoryRepository refreshHistoryRepository, IBudApiService budApiService)
		{
			_accountRepository = accountRepository;
			_refreshHistoryRepository = refreshHistoryRepository;
			_budApiService = budApiService;
		}

		public async override Task<ApiResult> Handle(InitiateRefresh.Command request, CancellationToken cancellationToken)
		{
			var providers = await _accountRepository.GetUserAccountsProviders(CustomerId.Parse(request.UserId), cancellationToken);
			var budApiRequests = new List<Task<BudBaseResponse<BudTaskResponse>>>();

			PopulateBudApiRequests(request, providers, budApiRequests, cancellationToken);

			await Task.WhenAll(budApiRequests);

			await _refreshHistoryRepository.AddRefresh(request.UserId, cancellationToken);
			await _refreshHistoryRepository.SaveChangesAsync(cancellationToken);

			return ApiResult.Success();
		}

		private void PopulateBudApiRequests(InitiateRefresh.Command request, IEnumerable<string> providers, List<Task<BudBaseResponse<BudTaskResponse>>> requests, CancellationToken cancellationToken)
		{
			foreach (var provider in providers)
			{
				var refreshRequest = new BudInitiateRefreshRequest(provider);

				requests.Add(_budApiService.PostToApi<BudBaseResponse<BudTaskResponse>, BudInitiateRefreshRequest>
					(BudApiEndpoints.InitiateRefresh, refreshRequest, request.BudCustomerId, request.BudCustomerSecret, cancellationToken));
			}
		}
	}
}
