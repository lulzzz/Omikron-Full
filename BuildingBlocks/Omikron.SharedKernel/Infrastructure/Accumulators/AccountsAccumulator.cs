using Omikron.SharedKernel.Infrastructure.Comparers;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Omikron.SharedKernel.Infrastructure.Accumulators
{
	public class AccountsAccumulator
    {
        public AccountsAccumulator(IEnumerable<Account> existingAccounts)
        {
            ExistingAccounts = existingAccounts;
            AccountsToUpdate = new List<Account>();
            AccountsToAdd = new List<Account>();
        }

        public IEnumerable<Account> ExistingAccounts { get; }
        public List<Account> AccountsToUpdate { get; }
        public List<Account> AccountsToAdd { get; }

        public AccountsAccumulator Accumulate(Account account)
        {
            var existingAccount = ExistingAccounts.FirstOrDefault(predicate: a => a.BudAccountId == account.BudAccountId);

            if (existingAccount == null)
            {
                var id = Guid.NewGuid();
                account.Id = id;
                account.ExternalId = id;
                AccountsToAdd.Add(item: account);
            }
            else
            {
                existingAccount.Currency = account.Currency;
                existingAccount.Name = account.Name;
                existingAccount.Provider = account.Provider;
                existingAccount.IdentificationNumber = account.IdentificationNumber;

                var newBalances = account.AccountBalances.Except(second: existingAccount.AccountBalances, comparer: new AccountBalanceComparer());
                newBalances.ForEach(action: nb => existingAccount.AccountBalances = existingAccount.AccountBalances.Append(element: nb).ToList());

                AccountsToUpdate.Add(item: existingAccount);
            }

            return this;
        }
    }
}
