using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class UpdateMarketingCommunications
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Username { get; set; }
            public bool ReceiveMarketingCommunications { get; set; }
        }
    }
}
