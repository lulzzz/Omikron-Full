using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.Sync.Bud.Commands
{
	public class InitiateRefresh
    {
        public class Command : TenantCommand<ApiResult>
		{
			public Guid UserId { get; set; }
			public string BudCustomerId { get; set; }
			public string BudCustomerSecret { get; set; }

			public Command(Guid userId, string budCustomerId, string budCustomerSecret)
			{
				UserId = userId;
				BudCustomerId = budCustomerId;
				BudCustomerSecret = budCustomerSecret;
			}
		}
	}
}
