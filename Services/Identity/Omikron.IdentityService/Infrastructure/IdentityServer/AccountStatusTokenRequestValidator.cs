using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Utils;

namespace Omikron.IdentityService.Infrastructure.IdentityServer
{
    public class TokenRequestValidator : ICustomTokenRequestValidator
    {
        private readonly string[] _byPassGrantTypes = {"refresh_token", GrantType.ClientCredentials};
        private readonly IdentityUserManager _userManager;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public TokenRequestValidator(IdentityUserManager userManager, IPhoneNumberRepository phoneNumberRepository)
        {
            _userManager = userManager;
            _phoneNumberRepository = phoneNumberRepository;
        }

        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            if (_byPassGrantTypes.Any(predicate: gt => gt.Equals(value: context.Result.ValidatedRequest.GrantType)))
            {
                return;
            }

            var username = context.Result.ValidatedRequest.UserName;

            var user = await _userManager.Users.FirstOrDefaultAsync(predicate: u => u.UserName == username || u.Email == username);
            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByEmailAsync(username);

            var validateUsersRememberMeData = !user.RememberMeAt.HasValue || ((Clock.GetTime() - user.RememberMeAt).Value.TotalDays > Constants.RememberMeValidDays);
            if (!phoneNumber.IdentityTokenAvailable && validateUsersRememberMeData)
            {
                context.Result.IsError = true;
                context.Result.Error = "sms_verification_not_completed";
                context.Result.ErrorDescription = $"SMS Verification has to be completed before you can login.";
                return;
            }

            var allowedStatus = new[] {AccountStatus.Active, AccountStatus.OnBoarding, AccountStatus.PerformKyc, AccountStatus.AddBankAccount};

            if (!allowedStatus.Contains(value: user.AccountStatus))
            {
                context.Result.IsError = true;
                context.Result.Error = "account_status_not_valid";

                switch (user.AccountStatus)
                {
                    case AccountStatus.Disabled:
                        context.Result.ErrorDescription = $"The account '{username}' does not have permission to login. Please, contact an administrator for further information.";
                        break;
                    case AccountStatus.New:
                        context.Result.ErrorDescription = $"The account '{username}' has not been confirmed. Please check your email for confirmation link. If problem persists please contact admin.";
                        break;
                }
            }

            phoneNumber.IdentityTokenAvailable = false;
            _phoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            var saved = await _phoneNumberRepository.SaveChangesAsync();

            if (!saved)
            {
                context.Result.IsError = true;
                context.Result.Error = "database_update_fail";
                context.Result.ErrorDescription = $"Database update failed. Please try again.";
            }
        }
    }
}