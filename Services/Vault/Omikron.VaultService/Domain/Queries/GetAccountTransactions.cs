using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;
using System.Collections.Generic;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetAccountTransactions
    {
		public class Query : PaginationQuery<ApiResult<IEnumerable<TransactionViewModelContainer>>>
		{
            public Guid AccountId { get; set; }
            public string SearchTerm { get; set; }

			public Query(Guid accountId, int page, string searchTerm)
            {
                AccountId = accountId;
                SearchTerm = searchTerm;
                Page = page;
                PageSize = 5;
            }
        }
    }
}
