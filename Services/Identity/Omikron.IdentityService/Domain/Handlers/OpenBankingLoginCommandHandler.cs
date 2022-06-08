using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class OpenBankingLoginCommandHandler : BaseHandlerLight<OpenBankingLogin.Command, ApiResult<ObLoginUrlResponse>>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IBudApiService _budApiService;

        public OpenBankingLoginCommandHandler(IdentityUserManager userManager, IBudApiService budApiService)
        {
            _userManager = userManager;
            _budApiService = budApiService;
        }
        public override async Task<ApiResult<ObLoginUrlResponse>> Handle(OpenBankingLogin.Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user.BudCustomerId == null || user.BudCustomerSecret == null)
            {
                return ApiResult<ObLoginUrlResponse>.BadRequest("Please perform KYC validation first.");
            }

            var obLoginRequest = new ObLoginRequest()
            {
                RequestUrl = new Uri(request.RedirectUrl),
                Providers = new List<string> { request.ProviderName }
            };
            var response = await _budApiService.PostToApi<BudBaseResponse<ObLoginUrlResponse>, ObLoginRequest>(BudApiEndpoints.RetrieveAuthorisationGatewayUrl, obLoginRequest, user.BudCustomerId, user.BudCustomerSecret, cancellationToken);

            return ApiResult<ObLoginUrlResponse>.Success().WithData(response.Data);
        }
    }
}
