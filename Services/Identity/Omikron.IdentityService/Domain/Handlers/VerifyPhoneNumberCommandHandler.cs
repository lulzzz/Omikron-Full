using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class VerifyPhoneNumberCommandHandler : BaseHandler<VerifyPhoneNumber.Command, ApiResult>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public VerifyPhoneNumberCommandHandler(IDispatcher dispatcher, IPhoneNumberRepository phoneNumberRepository) : base(dispatcher)
        {
            _phoneNumberRepository = phoneNumberRepository;
        }

        public async override Task<ApiResult> Handle(VerifyPhoneNumber.Command request, CancellationToken cancellationToken)
        {
            var lockoutRemaining = Constants.PhoneNumberTokenLockoutDuration - (DateTime.UtcNow - request.PhoneNumber.LockoutTime);
            if (request.PhoneNumber.LockedOut)
            {
                var lockedOutRunOut = Clock.GetTime() - request.PhoneNumber.LockoutTime > Constants.PhoneNumberTokenLockoutDuration;
                if (lockedOutRunOut)
                {
                    request.PhoneNumber.LockedOut = false;
                    _phoneNumberRepository.UpdatePhoneNumber(request.PhoneNumber);
                    await _phoneNumberRepository.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    return ApiResult.BadRequest($"Phone number has been locked out due to multiple invalid verification attempts. Please generate new code in: {lockoutRemaining.Minutes} minutes and {lockoutRemaining.Seconds} seconds");
                }
            }

            if (request.PhoneNumber.TokenExpired)
            {
                return ApiResult.BadRequest("Token for this phone number has expired. Please generate new one.");
            }

            if (request.PhoneNumber.Verified)
            {
                return ApiResult.BadRequest($"Phone number has already been verified.");
            }

            if (request.Token != request.PhoneNumber.Token)
            {
                request.PhoneNumber.VerificationAttempts++;

                if (request.PhoneNumber.VerificationAttempts >= Constants.MaximumAllowedVerificationAttempts)
                {
                    request.PhoneNumber.LockedOut = true;
                    request.PhoneNumber.TokenExpired = true;
                    request.PhoneNumber.VerificationAttempts = 0;
                    request.PhoneNumber.LockoutTime = DateTime.UtcNow;
                }

                _phoneNumberRepository.UpdatePhoneNumber(request.PhoneNumber);
                await _phoneNumberRepository.SaveChangesAsync(cancellationToken);
                return ApiResult.BadRequest($"Invalid token. Please try again.");
            }

            request.PhoneNumber.Verified = true;
            request.PhoneNumber.TokenExpired = true;
            request.PhoneNumber.VerificationAttempts = 0;
            _phoneNumberRepository.UpdatePhoneNumber(request.PhoneNumber);
            return ApiResult.Success();
        }
    }
}
