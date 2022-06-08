using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard;
using System;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetAccounts
    {
        public class Query : BaseCommand.Action<ApiResult<DashboardAccountGroupingViewModel>>
        {
            public Guid UserId { get; set; }

            public Query(Guid userId)
            {
                UserId = userId;
            }
        }
    }
}
