using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class VerifyPhoneNumber
    {
        public class Command : TenantCommand<ApiResult>
        {
            public PhoneNumber PhoneNumber { get; set; }
            public int Token { get; set; }

            public Command(PhoneNumber phoneNumber, int token)
            {
                PhoneNumber = phoneNumber;
                Token = token;
            }
        }
    }
}
