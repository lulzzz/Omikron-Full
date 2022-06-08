using Omikron.IdentityService.Domain.Abstraction;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class GenerateTokenForExistingPhoneNumber
    {
        public class Command : TenantCommand<ApiResult<string>>, IVerificationCodeSenderCommand<ApiResult<string>>
        {
            public PhoneNumber PhoneNumber { get; set; }

            public Command(PhoneNumber phoneNumber)
            {
                PhoneNumber = phoneNumber;
            }
        }
    }
}
