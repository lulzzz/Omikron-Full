using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using System.Collections.Generic;

namespace Omikron.Sync.Bud.Models
{
	public class CustomerAccountsData
    {
		public IEnumerable<BudListAccountsResponse> BudAccounts { get; set; }
		public IEnumerable<BudListCustomerConsentsResponse> Consents { get; set; }

		public CustomerAccountsData(IEnumerable<BudListAccountsResponse> budAccounts, IEnumerable<BudListCustomerConsentsResponse> consents)
		{
			BudAccounts = budAccounts;
			Consents = consents;
		}
	}
}
