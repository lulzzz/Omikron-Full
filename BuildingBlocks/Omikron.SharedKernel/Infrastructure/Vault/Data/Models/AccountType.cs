using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Vault.Extensions;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public class AccountType : Enumeration
	{
		public AccountType(int id, string name) : base(id, name)
		{
		}

		public static AccountType CurrentAccount => new(1, "CurrentAccount");
		public static AccountType Savings => new(2, "Savings");
		public static AccountType CreditCard => new(3, "CreditCard");
		public static AccountType Loan => new(4, "Loan");
		public static AccountType Pensions => new(5, "Pensions");
		public static AccountType Investments => new(6, "Investments");
		public static AccountType ChargeCard => new(7, "ChargeCard");
		public static AccountType EMoney => new(8, "EMoney");
		public static AccountType PrePaidCard => new(9, "PrePaidCard");

		public static AccountType Parse(int value)
		{
			return value switch
			{
				1 => CurrentAccount,
				2 => Savings,
				3 => CreditCard,
				4 => Loan,
				5 => Pensions,
				6 => Investments,
				7 => ChargeCard,
				8 => EMoney,
				9 => PrePaidCard,
				_ => throw new ArgumentOutOfRangeException(nameof(value))
			};
		}

		public static AccountType Parse(string value)
		{
			return value switch
			{
				"CurrentAccount" => CurrentAccount,
				"Current Accounts" => CurrentAccount,
				"Savings" => Savings,
				"Pensions" => Pensions,
				"Investments" => Investments,
				"CreditCard" => CreditCard,
				"Credit Cards" => CreditCard,
				"Loan" => Loan,
				"Loans" => Loan,
				"ChargeCard" => ChargeCard,
				"EMoney" => EMoney,
				"PrePaidCard" => PrePaidCard,
				_ => throw new ArgumentOutOfRangeException(nameof(value))
			};
		}

		public static implicit operator AccountType(string value)
		{
			return Parse(value);
		}

		public static implicit operator string(AccountType accountType)
		{
			return accountType.ToString();
		}

		public override string ToString()
		{
			return Name;
		}

		//TODO: Investigate how to do this with a loop in base class
		public static IEnumerable<string> EnumerateTypesAsDisplayNames()
		{
			return new List<string>()
			{
				CurrentAccount.ToString().ToAccountGroupDisplayName(),
				Savings.ToString().ToAccountGroupDisplayName(),
				CreditCard.ToString().ToAccountGroupDisplayName(),
				Loan.ToString().ToAccountGroupDisplayName(),
				Pensions.ToString().ToAccountGroupDisplayName(),
				Investments.ToString().ToAccountGroupDisplayName()
			};
		}
	}
}
