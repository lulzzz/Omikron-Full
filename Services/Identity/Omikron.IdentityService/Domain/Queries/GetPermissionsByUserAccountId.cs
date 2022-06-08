using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetPermissionsByUserAccountId
    {
        public class Query : BaseCommand.Action<ApiResult<IReadOnlyList<string>>>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }
    }
}