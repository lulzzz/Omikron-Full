using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class ResetPasswordCommandHandler : BaseHandler<ResetPassword.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public ResetPasswordCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IPhoneNumberRepository phoneNumberRepository) : base(dispatcher)
        {
            _userManager = userManager;
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult> Handle(ResetPassword.Command request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByEmailAsync(request.Email, cancellationToken);

            if (phoneNumber == null)
            {
                return ApiResult.NotFound($"Phone number associated with an email {request.Email} does not exist in the system.");
            }

            var verifiedPhoneNumber = await Dispatcher.DispatchAsync(new VerifyPhoneNumber.Command(phoneNumber, request.VerificationToken), cancellationToken);

            if (!verifiedPhoneNumber.IsSuccess)
            {
                return verifiedPhoneNumber;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(phoneNumber.Owner);
            var identityResult = await _userManager.ResetPasswordAsync(phoneNumber.Owner, resetToken, request.Password);

            return identityResult.Succeeded ? ApiResult.Success() : ApiResult.BadRequest(identityResult);
        }
    }
}
