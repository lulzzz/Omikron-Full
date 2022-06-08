using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Omikron.SharedKernel.Infrastructure.Comparers
{
	public class AccountBalanceComparer : IEqualityComparer<AccountBalance>
    {
        public bool Equals(AccountBalance x, AccountBalance y)
        {
            if (x is null)
			{
                return false;
			}

            if (y is null)
            {
                return false;
            }

            if (x.BudAccountId != y.BudAccountId)
            {
                return false;
            }

            if (x.Amount != y.Amount)
            {
                return false;
            }

            if (x.BalanceType != y.BalanceType)
            {
                return false;
            }

            if (x.CreditDebitIndicator != y.CreditDebitIndicator)
			{
                return false;
			}

            return true;
        }

        public int GetHashCode([DisallowNull] AccountBalance obj)
        {
            return base.GetHashCode();
        }
    }
}