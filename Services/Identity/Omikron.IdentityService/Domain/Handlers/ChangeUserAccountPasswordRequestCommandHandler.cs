using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.TenantService.Domain.Services;


namespace Omikron.IdentityService.Domain.Handlers
{
    public class ChangeUserAccountPasswordRequestCommandHandler : BaseHandler<ChangeUserAccountPasswordRequest.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IUserAccountService _userService;

        public ChangeUserAccountPasswordRequestCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IUserAccountService userService)
            :base(dispatcher)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public override async Task<ApiResult> Handle(ChangeUserAccountPasswordRequest.Command command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(command.Email);

            if (user == null || (!command.BypassEmailConfirmation && !user.EmailConfirmed))
            {
                return ApiResult.BadRequest($"The user '{command.Email}' does not exists.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            user.ConfirmationTokens.Add(new ConfirmationToken(ConfirmationTokenType.ResetPassword, token));

            await _userManager.UpdateAsync(user);

            await _userService.SendResetPasswordTokenEmailAsync(user, token, command.TenantIdentifier);

            return ApiResult.Success();
        }
    }
}