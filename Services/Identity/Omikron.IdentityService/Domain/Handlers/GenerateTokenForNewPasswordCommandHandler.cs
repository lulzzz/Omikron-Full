using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GenerateTokenForNewPasswordCommandHandler : BaseHandler<GenerateTokenForNewPassword.Command, ApiResult<string>>
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly IUserRepository _userRepository;

        public GenerateTokenForNewPasswordCommandHandler(IDispatcher dispatcher, IdentityUserManager identityUserManager, IUserRepository userRepository) : base(dispatcher)
        {
            _identityUserManager = identityUserManager;
            _userRepository = userRepository;
        }

        public async override Task<ApiResult<string>> Handle(GenerateTokenForNewPassword.Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsernameIncludePhoneNumberAsync(request.Username, cancellationToken);
            if (user == null)
            {
                return ApiResult<string>.BadRequest("User does not exist.");
            }

            var correctPassword = await _identityUserManager.CheckPasswordAsync(user, request.CurrentPassword);

            return correctPassword
                ? await Dispatcher.DispatchAsync(new GenerateTokenForExistingPhoneNumber.Command(user.PhoneNumberForVerification), cancellationToken)
                : ApiResult<string>.BadRequest("Invalid password");
        }
    }
}
