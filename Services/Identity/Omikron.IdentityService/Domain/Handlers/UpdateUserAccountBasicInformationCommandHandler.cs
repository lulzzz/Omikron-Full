using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;


namespace Omikron.IdentityService.Domain.Handlers
{
    public class UpdateUserAccountBasicInformationCommandHandler : BaseHandler<UpdateUserAccountBasicInformationByUsername.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;

        public UpdateUserAccountBasicInformationCommandHandler(IDispatcher dispatcher,
            IdentityUserManager userManager) : base(dispatcher)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult> Handle(UpdateUserAccountBasicInformationByUsername.Command command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(command.Username);
            if (user == null)
            {
                return ApiResult.NotFound($"The user '{command.Username}' does not exist.");
            }

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.PhoneNumber = command.PhoneNumber;

            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                return ApiResult.BadRequest(identityResult);
            }

            await Dispatcher.DispatchAsync(new UserUpdatedEvent(user.Id), cancellationToken);
            return ApiResult.Success();
        }
    }
}