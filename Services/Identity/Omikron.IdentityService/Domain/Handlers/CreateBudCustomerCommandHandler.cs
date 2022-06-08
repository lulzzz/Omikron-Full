using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
	public class CreateBudCustomerCommandHandler : BaseHandlerLight<CreateBudCustomer.Command, ApiResult>
	{
		private readonly IBudApiService _budApiService;
		private readonly IdentityUserManager _userManager;

		public CreateBudCustomerCommandHandler(IBudApiService budApiService, IdentityUserManager userManager)
		{
			_budApiService = budApiService;
			_userManager = userManager;
		}

		public override async Task<ApiResult> Handle(CreateBudCustomer.Command request, CancellationToken cancellationToken)
		{
			var budCustomer = await _budApiService.PostToApi<BudBaseResponse<CreateCustomerResponse>>(BudApiEndpoints.CreateCustomer, cancellationToken: cancellationToken);

			if (budCustomer.Data.CustomerId.IsNullOrEmpty() || budCustomer.Data.CustomerSecret.IsNullOrEmpty())
			{
				return ApiResult.BadRequest("Something went wrong. Please try again.");
			}

			request.User.BudCustomerId = budCustomer.Data.CustomerId;
			request.User.BudCustomerSecret = budCustomer.Data.CustomerSecret;
			request.User.AccountStatus = AccountStatus.AddBankAccount;

			var identityResult = await _userManager.UpdateAsync(request.User);

			return identityResult.Succeeded ? ApiResult.Success() : ApiResult.BadRequest(identityResult);
		}
	}
}
