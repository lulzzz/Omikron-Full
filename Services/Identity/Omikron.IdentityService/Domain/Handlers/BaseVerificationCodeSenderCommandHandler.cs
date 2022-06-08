using Omikron.IdentityService.Domain.Abstraction;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.SmsProvider;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public abstract class BaseVerificationCodeSenderCommandHandler<TRequest, TResponse> : BaseHandlerLight<TRequest, TResponse> where TRequest : IVerificationCodeSenderCommand<TResponse>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;
        private readonly ISmsProvider _smsProvider;

        public BaseVerificationCodeSenderCommandHandler(IPhoneNumberRepository phoneNumberRepository, ISmsProvider smsProvider)
        {
            _phoneNumberRepository = phoneNumberRepository;
            _smsProvider = smsProvider;
        }

        protected virtual bool TokenResendTimeExpired(PhoneNumber phoneNumber)
        {
            return Clock.GetTime() - phoneNumber.TokenCreationTime < Constants.PhoneNumberTokenResendTime;
        }

        protected virtual TimeSpan TimeElapsedSinceLockout(PhoneNumber phoneNumber)
        {
            return Constants.PhoneNumberTokenLockoutDuration - (Clock.GetTime() - phoneNumber.LockoutTime);
        }

        protected virtual void GenerateTokenAndUpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            phoneNumber.Token = FactoryToken();
            phoneNumber.TokenCreationTime = Clock.GetTime();
            phoneNumber.Verified = false;
            phoneNumber.VerificationAttempts = 0;
            phoneNumber.TokenExpired = false;
            phoneNumber.LockedOut = false;

            _phoneNumberRepository.UpdatePhoneNumber(phoneNumber);
        }

        protected virtual async Task<bool> CommitAndSendSms(string number, PhoneNumber phoneNumber, CancellationToken cancellationToken)
        {
            var success = await _phoneNumberRepository.SaveChangesAsync(cancellationToken);
            if (success)
            {
                await _smsProvider.SendSms(number, $"Omikron Verification code: {phoneNumber.Token}");
            }
            return success;
        }

        protected virtual int FactoryToken()
        {
            var random = new Random();
            var token = random.Next(Constants.VerificationTokenLowerBound, Constants.VerificationTokenUpperBound);
            return token;
        }
    }
}
