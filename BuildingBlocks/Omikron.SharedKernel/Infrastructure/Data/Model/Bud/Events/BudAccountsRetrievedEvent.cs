using Omikron.SharedKernel.Domain;
using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud.Events
{
	public class BudAccountsRetrievedEvent : BaseDomainEvent
	{
		public Guid UserId { get; set; }
		public IEnumerable<BudListAccountsResponse> BudAccounts { get; set; }
		public IEnumerable<BudListCustomerConsentsResponse> CustomerConsents { get; set; }

		public BudAccountsRetrievedEvent(Guid userId, IEnumerable<BudListAccountsResponse> budAccounts, IEnumerable<BudListCustomerConsentsResponse> customerConsents)
		{
			UserId = userId;
			BudAccounts = budAccounts;
			CustomerConsents = customerConsents;
		}
	}
}
