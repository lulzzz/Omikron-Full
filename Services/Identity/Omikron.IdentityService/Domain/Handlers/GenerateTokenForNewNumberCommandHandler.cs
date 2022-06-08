using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.SmsProvider;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GenerateTokenForNewNumberCommandHandler : BaseVerificationCodeSenderCommandHandler<GenerateTokenForNewNumber.Command, ApiResult<string>>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public GenerateTokenForNewNumberCommandHandler(IPhoneNumberRepository phoneNumberRepository, ISmsProvider smsProvider) : base(phoneNumberRepository, smsProvider)
        {
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult<string>> Handle(GenerateTokenForNewNumber.Command request, CancellationToken cancellationToken)
        {
            var existingPhoneNumber = await _phoneNumberRepository.GetPhoneNumberByUserIdAsync(request.UserId, cancellationToken);

            if (TokenResendTimeExpired(existingPhoneNumber))
            {
                return ApiResult<string>.BadRequest("You have to wait 15 seconds before you can generate new token.");
            }

            var lockoutRemaining = TimeElapsedSinceLockout(existingPhoneNumber);

            if (existingPhoneNumber.LockedOut && lockoutRemaining > TimeSpan.Zero)
            {
                return ApiResult<string>.BadRequest($"Phone number has been locked out due to multiple invalid verification attempts. Please generate new code in: {lockoutRemaining.Minutes} minutes and {lockoutRemaining.Seconds} seconds");
            }

            GenerateTokenAndUpdatePhoneNumber(existingPhoneNumber);

            var success = await CommitAndSendSms(request.PhoneNumber, existingPhoneNumber, cancellationToken);

            return success ? ApiResult<string>.Success().WithData(request.PhoneNumber.HidePartOfThePhoneNumber()) : ApiResult<string>.BadRequest("Failed to generate code");
        }
    }
}
