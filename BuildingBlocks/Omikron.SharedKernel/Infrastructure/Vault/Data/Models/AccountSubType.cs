using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public class AccountSubType : Enumeration
    {
		public AccountSubType(int id, string name) : base(id, name)
        {
        }

        public static AccountSubType Asset => new(1, "Asset");
        public static AccountSubType Liability => new(2, "Liability");

        public static AccountSubType Parse(int value)
        {
            return value switch
            {
                1 => Asset,
                2 => Liability,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static AccountSubType Parse(string value)
        {
            return value switch
            {
                "Asset" => Asset,
                "Liability" => Liability,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static AccountSubType Parse(decimal value)
        {
            return value >= 0 ? Asset : Liability;
        }

        public static AccountSubType Parse(string type, decimal value)
        {
            if (type == AccountType.CurrentAccount)
			{
                return value >= 0 ? Asset : Liability;
			}
            if (type == AccountType.Savings)
			{
                return Asset;
			}
            if (type == AccountType.Pensions)
            {
                return Asset;
            }
            if (type == AccountType.Investments)
            {
                return Asset;
            }
            if (type == AccountType.CreditCard)
            {
                return Liability;
            }
            if (type == AccountType.Loan)
            {
                return Liability;
            }
            if (type == AccountType.ChargeCard)
            {
                return Liability;
            }
            if (type == AccountType.EMoney)
            {
                return Asset;
            }
            if (type == AccountType.PrePaidCard)
            {
                return Liability;
            }
            if (type == "FinancialAgreement")
			{
                return Liability;
			}
            if (type == "Mortgage")
            {
                return Liability;
            }

            throw new ArgumentOutOfRangeException("Unknown AccountType provided");
        }

        public static AccountSubType Parse(AccountType accountType)
        {
            return accountType == AccountType.CreditCard || accountType == AccountType.Loan ? Liability : Asset;
        }

        public static implicit operator AccountSubType(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(AccountSubType accountType)
        {
            return accountType.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}