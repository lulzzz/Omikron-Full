using System.Collections.Generic;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetUserAccounts
    {
        public class Query : PaginationQuery<ApiResult<IReadOnlyList<UserAccountViewModel>>>
        {
            public Query()
            {
            }

            public Query(int page, string keyword)
            {
                Keyword = keyword;
                Page = page;
            }

            public string Keyword { get; set; }
        }
    }
}