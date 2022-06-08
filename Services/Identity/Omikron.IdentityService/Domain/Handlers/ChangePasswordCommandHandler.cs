using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class ChangePasswordCommandHandler : BaseHandler<ChangePassword.Command, ApiResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IdentityUserManager _identityUserManager;

        public ChangePasswordCommandHandler(IDispatcher dispatcher, IUserRepository userRepository, IdentityUserManager identityUserManager) : base(dispatcher)
        {
            _userRepository = userRepository;
            _identityUserManager = identityUserManager;
        }

        public async override Task<ApiResult> Handle(ChangePassword.Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsernameIncludePhoneNumberAsync(request.Username, cancellationToken);
            if (user == null)
            {
                return ApiResult.BadRequest("User does not exist.");
            }

            var correctPassword = await _identityUserManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!correctPassword)
            {
                return ApiResult.BadRequest("Invalid password.");
            }

            var tokenVerification = await Dispatcher.DispatchAsync(new VerifyPhoneNumber.Command(user.PhoneNumberForVerification, request.VerificationToken), cancellationToken);
            if (!tokenVerification.IsSuccess)
            {
                return ApiResult.BadRequest(tokenVerification.Errors);
            }

            var passwordChanged = await _identityUserManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!passwordChanged.Succeeded)
            {
                return ApiResult.BadRequest("Something went wrong. Please try again later or contact us.");
            }

            await Dispatcher.DispatchAsync(new UserPasswordChangedEvent(user.Id), cancellationToken);

            return ApiResult.Success();
        }
    }
}
