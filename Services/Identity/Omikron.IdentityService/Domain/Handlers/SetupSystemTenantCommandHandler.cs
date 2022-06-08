using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class SetupSystemTenantCommandHandler : BaseHandlerLight<SetupSystemUserAccountCommand, EmptyResult>
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityUserManager _userManager;

        public SetupSystemTenantCommandHandler(IConfiguration configuration, IdentityUserManager userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public override async Task<EmptyResult> Handle(SetupSystemUserAccountCommand command, CancellationToken cancellationToken)
        {
            var user = _configuration.GetSection(key: "System:User").Get<User>();
            var password = _configuration.GetValue<string>(key: "System:User:Password");

            var exist = await _userManager.Users.AnyAsync(predicate: u => u.UserName.Equals(user.UserName), cancellationToken: cancellationToken);
            if (!exist)
            {

                user.Email = user.UserName;
                user.EmailConfirmed = true;
                user.AccountStatus = AccountStatus.Active;
                user.PhoneNumberId = user.ExternalId;
                user.RememberMeAt = Clock.GetTime();

                var phoneNumber = _configuration.GetSection(key: "System:PhoneNumber").Get<PhoneNumber>();
                phoneNumber.Id = user.ExternalId;
                phoneNumber.TokenCreationTime = Clock.GetTime();

                user.PhoneNumberForVerification = phoneNumber;

                var result = await _userManager.CreateAsync(user: user, password: password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user: user, role: RoleConstants.SystemTenantAdministratorRole);
                }
            }

            return EmptyResult.Value;
        }
    }
}