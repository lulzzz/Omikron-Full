using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Messaging;
using System;
using System.Collections.Generic;

namespace Omikron.Sync.Bud.Commands
{
	public class SyncTransactions
	{
		public class Command : TenantCommand<ApiResult>
		{
			public Guid UserId { get; set; }
			public IEnumerable<BudListTransactionsResponse> BudTransactions { get; set; }

			public Command(Guid userId, IEnumerable<BudListTransactionsResponse> budTransactions)
			{
				UserId = userId;
				BudTransactions = budTransactions;
			}
		}
	}
}
