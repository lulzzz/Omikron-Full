using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class ActivateRememberMeCommandHandler : BaseHandler<ActivateRememberMe.Command, ApiResult<string>>
    {
        private readonly IdentityUserManager _userManager;

        public ActivateRememberMeCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager) : base(dispatcher)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult<string>> Handle(ActivateRememberMe.Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return ApiResult<string>.BadRequest($"User with username: {request.UserName} does not exist.");
            }

             user.RememberMeAt = request.RememberMeAt;
             var saveChanges = await _userManager.UpdateAsync(user);

            return saveChanges.Succeeded  ? ApiResult<string>.Success() : ApiResult<string>.BadRequest().WithData(saveChanges.Errors.ToString());
        }
    }
}