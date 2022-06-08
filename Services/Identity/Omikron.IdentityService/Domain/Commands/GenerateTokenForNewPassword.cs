using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class GenerateTokenForNewPassword
    {
        public class Command : TenantCommand<ApiResult<string>>
        {
            public string Username { get; set; }
            public string CurrentPassword { get; set; }
        }
    }
}
