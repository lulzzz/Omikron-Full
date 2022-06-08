using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Services
{
	public interface IAccountService
    {
        decimal CalculateTotalBalance(IEnumerable<Account> accounts);
        decimal CalculateTotalBalance(IEnumerable<VaultItem> items);
        decimal CalculateTotalValue(IEnumerable<Property> items);
        decimal CalculateTotalValue(IEnumerable<Vehicle> items);
        decimal CalculateTotalValue(IEnumerable<PersonalItem> items);
        decimal CalculateTotalValue(IEnumerable<Investment> items);
        decimal GetLastValueOrZero(Vehicle account);
        decimal GetLastValueOrZero(Property account);
        AccountBalance GetLastBalance(Account account);
        AccountBalance GetLastBalance(IEnumerable<AccountBalance> accountBalances, AccountType accountType);
        GetSummaryViewModel GetTotalAssetsAndLiabilities(IEnumerable<Account> accounts);
        void FactoryAccountBalanceHistory(Guid parentAccountId, AccountType type, List<AccountBalance> balances, DateTime currentDate, DateTime openDate, decimal currentBalance, decimal openBalance);
    }
}