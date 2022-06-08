using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.SmsProvider;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class SendVerificationCodeCommandHandler : BaseVerificationCodeSenderCommandHandler<SendVerificationCode.Command, ApiResult>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public SendVerificationCodeCommandHandler(IPhoneNumberRepository phoneNumberRepository, ISmsProvider smsProvider) : base(phoneNumberRepository, smsProvider)
        {
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult> Handle(SendVerificationCode.Command request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByEmailAsync(request.Email, cancellationToken);

            if (phoneNumber == null)
            {
                phoneNumber = FactoryPhoneNumber(request);
                await _phoneNumberRepository.AddPhoneNumberAsync(phoneNumber, cancellationToken);
            }
            else
            {
                if(request.PhoneNumber != phoneNumber.Number)
                {
                    return ApiResult.BadRequest($"Phone number connected to user {phoneNumber.Owner.UserName} is not correct. Please contact the Administator.");
                }

                if (TokenResendTimeExpired(phoneNumber))
                {
                    return ApiResult.BadRequest("You have to wait 15 seconds before you can generate new token.");
                }

                var lockoutRemaining = TimeElapsedSinceLockout(phoneNumber);

                if (phoneNumber.LockedOut && lockoutRemaining > TimeSpan.Zero)
                {
                    return ApiResult.BadRequest($"Phone number has been locked out due to multiple invalid verification attempts. Please generate new code in: {lockoutRemaining.Minutes} minutes and {lockoutRemaining.Seconds} seconds");
                }

                GenerateTokenAndUpdatePhoneNumber(phoneNumber);
            }

            var success = await CommitAndSendSms(phoneNumber.Number, phoneNumber, cancellationToken);
            return success ? ApiResult.Success() : ApiResult.BadRequest("Failed to generate code");
        }

        private PhoneNumber FactoryPhoneNumber(SendVerificationCode.Command sendVerificationCodeCommand)
        {
            return new PhoneNumber()
            {
                Id = Guid.NewGuid(),
                Number = sendVerificationCodeCommand.PhoneNumber,
                Token = FactoryToken(),
                TokenCreationTime = Clock.GetTime(),
                VerificationAttempts = 0
            };
        }
    }
}
