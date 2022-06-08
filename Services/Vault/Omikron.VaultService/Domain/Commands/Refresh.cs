using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.VaultService.Domain.Commands
{
    public class Refresh
    {
        public class Command : TenantCommand<ApiResult<RefreshHistoryViewModel>>
        {
            public Guid UserId { get; set; }

            public Command(Guid userId)
            {
                UserId = userId;
            }
        }
    }
}
