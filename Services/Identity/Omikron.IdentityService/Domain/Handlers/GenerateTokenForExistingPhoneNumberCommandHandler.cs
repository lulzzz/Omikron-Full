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
    public class GenerateTokenForExistingPhoneNumberCommandHandler : BaseVerificationCodeSenderCommandHandler<GenerateTokenForExistingPhoneNumber.Command, ApiResult<string>>
    {
        public GenerateTokenForExistingPhoneNumberCommandHandler(IPhoneNumberRepository phoneNumberRepository, ISmsProvider smsProvider) : base(phoneNumberRepository, smsProvider)
        {
        }

        public async override Task<ApiResult<string>> Handle(GenerateTokenForExistingPhoneNumber.Command request, CancellationToken cancellationToken)
        {
            if (TokenResendTimeExpired(request.PhoneNumber))
            {
                return ApiResult<string>.BadRequest("You have to wait 15 seconds before you can generate new token.");
            }

            var lockoutRemaining = TimeElapsedSinceLockout(request.PhoneNumber);

            if (request.PhoneNumber.LockedOut && lockoutRemaining > TimeSpan.Zero)
            {
                return ApiResult<string>.BadRequest($"Phone number has been locked out due to multiple invalid verification attempts. Please generate new code in: {lockoutRemaining.Minutes} minutes and {lockoutRemaining.Seconds} seconds");
            }

            GenerateTokenAndUpdatePhoneNumber(request.PhoneNumber);

            var success = await CommitAndSendSms(request.PhoneNumber.Number, request.PhoneNumber, cancellationToken);

            return success ? ApiResult<string>.Success().WithData(request.PhoneNumber.Number.HidePartOfThePhoneNumber()) : ApiResult<string>.BadRequest("Failed to generate code");
        }
    }
}
