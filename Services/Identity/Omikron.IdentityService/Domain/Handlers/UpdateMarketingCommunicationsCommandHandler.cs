using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class UpdateMarketingCommunicationsCommandHandler : BaseHandlerLight<UpdateMarketingCommunications.Command, ApiResult>
    {
        private readonly IdentityUserManager _identityUserManager;

        public UpdateMarketingCommunicationsCommandHandler(IdentityUserManager identityUserManager)
        {
            _identityUserManager = identityUserManager;
        }
        public async override Task<ApiResult> Handle(UpdateMarketingCommunications.Command request, CancellationToken cancellationToken)
        {
            var user = await _identityUserManager.FindByEmailAsync(request.Username);
            if (user == null)
            {
                return ApiResult.NotFound("User cannot be found.");
            }

            if (user.MarketingCommunications == request.ReceiveMarketingCommunications)
            {
                return ApiResult.Success();
            }

            user.MarketingCommunications = request.ReceiveMarketingCommunications;
            var updateResult = await _identityUserManager.UpdateAsync(user);

            return updateResult.Succeeded ? ApiResult.Success() : ApiResult.BadRequest("User update failed. Please try again later.");
        }
    }
}
