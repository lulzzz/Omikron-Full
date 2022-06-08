using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.VaultService.Domain.Commands
{
    public class RevokeConsent
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Command(string username, string provider)
            {
                Username = username;
                Provider = provider;
            }

            public string Username { get; set; }
            public string Provider { get; set; }
        }
    }
}
