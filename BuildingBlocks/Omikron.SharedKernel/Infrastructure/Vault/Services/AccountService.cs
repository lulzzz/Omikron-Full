using System;
using System.Collections.Generic;
using System.Linq;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Services
{
	public class AccountService : IAccountService
	{
		public decimal CalculateTotalBalance(IEnumerable<Account> accounts)
		{
			return accounts.Sum(a =>
			{
				var lastBalance = GetLastBalance(a);
				var lastBalanceAmount = lastBalance != null ? lastBalance.Amount : 0;
				var lastBalanceCreditDebitIndicator = lastBalance != null ? lastBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;
				return lastBalanceCreditDebitIndicator == CreditDebitIndicator.Credit ? lastBalanceAmount : lastBalanceAmount * (-1);
			});
		}
		public decimal CalculateTotalBalance(IEnumerable<VaultItem> items)
		{
			return items.Sum(a => a.CreditDebitIndicator == CreditDebitIndicator.Credit ? a.Value : a.Value * (-1));
		}

		public decimal CalculateTotalValue(IEnumerable<Property> items)
		{
			return items.Sum(p => GetLastValueOrZero(p));
		}

		public decimal CalculateTotalValue(IEnumerable<Vehicle> items)
		{
			return items.Sum(v => GetLastValueOrZero(v));
		}

		public decimal CalculateTotalValue(IEnumerable<PersonalItem> items)
		{
			return items.Sum(p => GetLastValueOrZero(p));
		}

		public decimal CalculateTotalValue(IEnumerable<Investment> items)
		{
			return items.Sum(p => GetLastValueOrZero(p));
		}

		public AccountBalance GetLastBalance(Account account)
		{
			return GetLastBalance(account.AccountBalances, account.Type);
		}

		public AccountBalance GetLastBalance(IEnumerable<AccountBalance> accountBalances, AccountType accountType)
		{
			if (accountBalances == null || !accountBalances.Any())
			{
				throw new ArgumentException("Account balances not available");
			}

			if (accountType == AccountType.CreditCard)
			{
				accountBalances = accountBalances.Where(b => b.BalanceType != BalanceType.ForwardAvailable &&
															 b.BalanceType != BalanceType.InterimAvailable &&
															 b.BalanceType != BalanceType.OpeningAvailable &&
															 b.BalanceType != BalanceType.PreviouslyClosedBooked);
			}

			if (accountBalances == null || !accountBalances.Any())
			{
				return null;
			}

			var bookedBalance = accountBalances.Where(a => a.BalanceType == Constants.PrimaryBalanceType).OrderBy(a => a.EntryDate).LastOrDefault();
			var availableBalance = accountBalances.Where(a => a.BalanceType == Constants.SecondaryBalanceType).OrderBy(a => a.EntryDate).LastOrDefault();

			if (bookedBalance != null && availableBalance == null)
			{
				return bookedBalance;
			}

			if (bookedBalance == null && availableBalance != null)
			{
				return availableBalance;
			}

			if (bookedBalance != null && availableBalance != null)
			{
				var bookedBalanceActualValue = bookedBalance.CreditDebitIndicator == CreditDebitIndicator.Debit ? bookedBalance.Amount * (-1) : bookedBalance.Amount;
				var availableBalanceActualValue = availableBalance.CreditDebitIndicator == CreditDebitIndicator.Debit ? availableBalance.Amount * (-1) : availableBalance.Amount;

				return bookedBalanceActualValue < availableBalanceActualValue ? bookedBalance : availableBalance;
			}

			var lastAccountBalances = accountBalances
				.GroupBy(b => b.EntryDate.ToString("yyyy-MM-dd HH-mm"))
				.OrderBy(g => g.Key)
				.LastOrDefault();

			var lowestBalance = lastAccountBalances.FirstOrDefault();

			foreach (var balance in lastAccountBalances)
			{
				var balanceAmount = balance.CreditDebitIndicator == CreditDebitIndicator.Debit ? balance.Amount * (-1) : balance.Amount;
				var lowestBalanceAmount = lowestBalance.CreditDebitIndicator == CreditDebitIndicator.Debit ? lowestBalance.Amount * (-1) : lowestBalance.Amount;

				if (balanceAmount < lowestBalanceAmount)
				{
					lowestBalance = balance;
				}
			}

			return lowestBalance;
		}

		public decimal GetLastValueOrZero(Property account)
		{
			return account.PropertyValues.Any() ?
				account.PropertyValues.OrderBy(a => a.EntryDate).Last().Amount :
				decimal.Zero;
		}

		public decimal GetLastValueOrZero(Vehicle account)
		{
			return account.VehicleValues.Any() ?
				account.VehicleValues.OrderBy(a => a.EntryDate).Last().Amount :
				decimal.Zero;
		}

		public decimal GetLastValueOrZero(Investment account)
		{
			return account.InvestmentValues.Any() ?
				account.InvestmentValues.OrderBy(a => a.EntryDate).Last().Amount :
				decimal.Zero;
		}

		public decimal GetLastValueOrZero(PersonalItem account)
		{
			return account.PersonalItemValues.Any() ?
				account.PersonalItemValues.OrderBy(a => a.EntryDate).Last().Amount :
				decimal.Zero;
		}

		public GetSummaryViewModel GetTotalAssetsAndLiabilities(IEnumerable<Account> accounts)
		{
			var result = new GetSummaryViewModel();
			accounts.GroupBy(a => a.Type).ForEach(grouping => Computation(grouping, result));
			return result;
		}

		private void Computation(IEnumerable<Account> grouping, GetSummaryViewModel viewModel)
		{
			var total = CalculateTotalBalance(grouping);

			if (total < 0)
			{
				viewModel.Liabilities += total;
			}
			else
			{
				viewModel.Assets += total;
			}
		}

		public void FactoryAccountBalanceHistory(Guid parentAccountId, AccountType type, List<AccountBalance> balances, DateTime currentDate, DateTime openDate, decimal currentBalance, decimal openBalance)
		{
			var monthDifference = currentDate.MonthDifference(openDate);
			var balanceTrend = monthDifference > 0 ? monthDifference.TrendOverRange(openBalance, currentBalance) : decimal.Zero;

			for (var i = 0; i < monthDifference; i++)
			{
				var amount = openBalance > currentBalance ? openBalance - balanceTrend * i : openBalance + balanceTrend * i;
				var date = openDate.AddMonths(i);

				var balance = new AccountBalance()
				{
					Amount = Math.Abs(amount),
					AccountId = parentAccountId,
					EntryDate = date,
					BalanceType = Constants.PrimaryBalanceType
				};

				if (type == AccountType.CurrentAccount)
				{
					balance.CreditDebitIndicator = amount > 0 ? CreditDebitIndicator.Credit : CreditDebitIndicator.Debit;
				}
				else
				{
					var accountSubType = AccountSubType.Parse(type);
					balance.CreditDebitIndicator = accountSubType == AccountSubType.Asset ? CreditDebitIndicator.Credit : CreditDebitIndicator.Debit;
				}

				balances.Add(balance);
			}
		}
	}
}