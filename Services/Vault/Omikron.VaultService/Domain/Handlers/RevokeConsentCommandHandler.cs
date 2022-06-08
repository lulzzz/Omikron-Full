using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class RevokeConsentCommandHandler : BaseHandlerLight<RevokeConsent.Command, ApiResult>
	{
		private readonly IBudApiService _budApiService;
		private readonly IAccountRepository _accountRepository;
		private readonly IHttpIdentityService _httpIdentityService;
		private readonly IVaultItemRepository _vaultItemRepository;

		public RevokeConsentCommandHandler(IBudApiService budApiService, IAccountRepository accountRepository, IHttpIdentityService httpIdentityService, IVaultItemRepository vaultItemRepository)
		{
			_budApiService = budApiService;
			_accountRepository = accountRepository;
			_httpIdentityService = httpIdentityService;
			_vaultItemRepository = vaultItemRepository;
		}

		public override async Task<ApiResult> Handle(RevokeConsent.Command request, CancellationToken cancellationToken)
		{
			var user = await _httpIdentityService.GetUserByUsername<VaultUserViewModel>(request.Username, cancellationToken);

			if (user.Records == null)
			{
				return ApiResult.BadRequest(user.Errors);
			}

			var revokeConsentRequest = new RevokeConsentRequest(request.Provider);
			var revokeTaskId = await _budApiService.PostToApi<BudBaseResponse<BudTaskResponse>, RevokeConsentRequest>
				(BudApiEndpoints.InitiateRevokeConsent, revokeConsentRequest, user.Records.BudCustomerId, user.Records.BudCustomerSecret, cancellationToken);

			var revokeResponse = await GetRevokeResponse(revokeTaskId.Data.TaskId, user.Records, cancellationToken);
			if (revokeResponse.Metadata.Status != Constants.RevokeCompleted)
			{
				return ApiResult.BadRequest("We couldn't remove consent. Please try again or contact an administrator.");
			}

			await RemoveProviderData(request, user, cancellationToken);

			var accountsToRemove = await _accountRepository.GetUserAccountsByProvider(CustomerId.Parse(user.Records.Id), request.Provider, cancellationToken);
			var vaultItemsToRemove = await _vaultItemRepository.GetUserAccountsByProvider(CustomerId.Parse(user.Records.Id), request.Provider, cancellationToken);

			if (accountsToRemove.Any())
			{
				_accountRepository.RemoveRange(accountsToRemove);
			}

			if (vaultItemsToRemove.Any())
			{
				_vaultItemRepository.RemoveRange(vaultItemsToRemove);
			}

			await _accountRepository.SaveChangesAsync(cancellationToken);

			return ApiResult.Success();
		}

		private async Task RemoveProviderData(RevokeConsent.Command request, ApiResult<VaultUserViewModel> user, CancellationToken cancellationToken)
		{
			var removeProviderDataUrl = $"{BudApiEndpoints.RemoveProviderData}/{request.Provider}";
			await _budApiService.DeleteFromApi(removeProviderDataUrl, user.Records.BudCustomerId, user.Records.BudCustomerSecret, cancellationToken);
		}

		//TODO: This code is duplicate from PerformKycCommandHandler. Create separate service for 'Exponential Backoff' requests.
		private async Task<BudMetadataResponse<RetrieveRevokeConsentStatusMetadataResponse>> GetRevokeResponse(string taskId, VaultUserViewModel user, CancellationToken cancellationToken)
		{
			var revokeConsentStatusEndpoint = $"{BudApiEndpoints.RetrieveRevokeConsentStatus}/{taskId}";

			var revokeConsentResponse = await _budApiService.GetFromApi<BudMetadataResponse<RetrieveRevokeConsentStatusMetadataResponse>>(revokeConsentStatusEndpoint, user.BudCustomerId, user.BudCustomerSecret, cancellationToken: cancellationToken);
			if (revokeConsentResponse.Metadata.Status == Constants.RevokeCompleted)
			{
				return revokeConsentResponse;
			}

			var wait = 0;
			for (var i = 1; i <= 5; i++)
			{
				revokeConsentResponse = await _budApiService.GetFromApi<BudMetadataResponse<RetrieveRevokeConsentStatusMetadataResponse>>(revokeConsentStatusEndpoint, user.BudCustomerId, user.BudCustomerSecret, cancellationToken: cancellationToken);
				if (revokeConsentResponse.Metadata.Status == Constants.RevokeCompleted)
				{
					break;
				}

				wait += SharedKernel.Constants.BackoffFactorInMilliseconds;

				await Task.Delay(wait, cancellationToken);
			}

			return revokeConsentResponse;
		}
	}
}