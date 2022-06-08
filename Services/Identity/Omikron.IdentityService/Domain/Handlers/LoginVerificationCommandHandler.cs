using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class LoginVerificationCommandHandler : BaseHandler<LoginVerification.Command, ApiResult>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public LoginVerificationCommandHandler(IDispatcher dispatcher, IPhoneNumberRepository phoneNumberRepository) : base(dispatcher)
        {
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult> Handle(LoginVerification.Command request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByEmailAsync(request.UserName, cancellationToken);
            if (phoneNumber == null)
            {
                return ApiResult.BadRequest($"Phone number associated with {request.UserName} does not exist");
            }

            var verification = await Dispatcher.DispatchAsync(new VerifyPhoneNumber.Command(phoneNumber, request.Token), cancellationToken);
            if (!verification.IsSuccess)
            {
                return verification;
            }

            phoneNumber.IdentityTokenAvailable = true;
            _phoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            var saved = await _phoneNumberRepository.SaveChangesAsync(cancellationToken);

            return saved ? ApiResult.Success() : ApiResult.BadRequest($"Database update fail. Please try again.");
        }
    }
}
