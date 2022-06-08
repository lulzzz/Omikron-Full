using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class DeleteUserQueryHandler : BaseHandlerLight<DeleteUser.Query, ApiResult>
    {
        private readonly IdentityUserManager _identityUserManager;

        public DeleteUserQueryHandler(IdentityUserManager identityUserManager)
        {
            _identityUserManager = identityUserManager;
        }
        public override async Task<ApiResult> Handle(DeleteUser.Query request, CancellationToken cancellationToken)
        {
            var user = await _identityUserManager.FindByNameAsync(request.User);
            if (user == null)
            {
                return ApiResult.BadRequest("Invalid username");
            }

          await _identityUserManager.DeleteAsync(user);
          return ApiResult.Success();
        }
    }
}