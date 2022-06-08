using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Microsoft.EntityFrameworkCore;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class UpdateUserAccountStatusHandler: BaseHandlerLight<UpdateUserAccountStatus.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;

        public UpdateUserAccountStatusHandler(IdentityUserManager userManager)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult> Handle(UpdateUserAccountStatus.Command command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.ExternalId == command.UserId, cancellationToken: cancellationToken);
          
            if (user == null)
            {
                return ApiResult.NotFound($"The user with id '{command.UserId}' does not exist.");
            }

            user.AccountStatus = command.Status;
            await _userManager.UpdateAsync(user);

            return ApiResult.Success();
        }
    }
}