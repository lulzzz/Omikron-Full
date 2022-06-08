using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Extensions
{
	public static class StringExtensions
	{
		public static string ToAccountGroupDisplayName(this string group)
		{
			if (group == AccountType.CurrentAccount)
			{
				return "Current Accounts";
			}
			if (group == AccountType.Savings)
			{
				return "Savings";
			}
			if (group == AccountType.Pensions)
			{
				return "Pensions";
			}
			if (group == AccountType.Investments)
			{
				return "Investments";
			}
			if (group == AccountType.CreditCard)
			{
				return "Credit Cards";
			}
			if (group == AccountType.Loan)
			{
				return "Loans";
			}
			if (group == AccountType.ChargeCard)
			{
				return "Charge Cards";
			}
			if (group == AccountType.EMoney)
			{
				return "EMoney";
			}
			if (group == AccountType.PrePaidCard)
			{
				return "Pre-Paid Cards";
			}
			if (group == VaultItemType.Property)
			{
				return "Properties";
			}
			if (group == VaultItemType.Vehicle)
			{
				return "Vehicles";
			}
			if (group == VaultItemType.PersonalItem)
			{
				return "Personal Items";
			}
			if (group == VaultItemType.Investment)
			{
				return "Investments";
			}
			if (group == LoanType.FinancialAgreement)
			{
				return "Finance Agreements";
			}
			if (group == LoanType.Mortgage)
			{
				return "Mortgages";
			}

			throw new ArgumentOutOfRangeException("Unknown group provided");
		}
	}
}
