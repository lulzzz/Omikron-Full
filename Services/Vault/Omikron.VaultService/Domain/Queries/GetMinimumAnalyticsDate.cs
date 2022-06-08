using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System;

namespace Omikron.VaultService.Domain.Queries
{
	public class GetMinimumAnalyticsDate
    {
        public class Query : BaseCommand.Action<ApiResult<DateTime>>
        {
            public Guid UserId { get; set; }

            public Query(Guid userId)
            {
                UserId = userId;
            }
        }
    }
}
