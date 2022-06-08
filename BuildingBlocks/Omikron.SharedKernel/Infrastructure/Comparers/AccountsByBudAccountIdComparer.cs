using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Omikron.SharedKernel.Infrastructure.Comparers
{
	public class AccountsByBudAccountIdComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account x, Account y)
        {
            return x.BudAccountId == y.BudAccountId;
        }

        public int GetHashCode([DisallowNull] Account obj)
        {
            return base.GetHashCode();
        }
    }
}
