using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;
using System.Collections.Generic;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetAvailableLoans
    {
        public class Query : BaseCommand.Action<ApiResult<IEnumerable<LoanViewModel>>>
		{
			public Guid UserId { get; set; }
			public string Search { get; set; }

			public Query(Guid userId, string search)
			{
				UserId = userId;
				Search = search;
			}
		}
	}
}
