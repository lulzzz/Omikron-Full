using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetLastRefresh
    {
        public class Query : BaseCommand.Action<ApiResult<RefreshHistoryViewModel>>
        {
            public Query(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }
    }
}
