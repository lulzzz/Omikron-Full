using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class UpdateNotifications
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Username { get; set; }
            public bool ReceiveAccountNotifications { get; set; }
        }
    }
}
