using Omikron.SharedKernel.Domain;
using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public class CreditDebitIndicator : Enumeration
    {
        private CreditDebitIndicator(int id, string name) : base(id, name)
        {
        }

        public static CreditDebitIndicator Credit => new(1, "Credit");
        public static CreditDebitIndicator Debit => new(2, "Debit");

        public static CreditDebitIndicator Parse(int value)
        {
            return value switch
            {
                1 => Credit,
                2 => Debit,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static CreditDebitIndicator Parse(string value)
        {
            return value switch
            {
                "Credit" => Credit,
                "Debit" => Debit,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator CreditDebitIndicator(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(CreditDebitIndicator creditDebitIndicator)
        {
            return creditDebitIndicator.ToString();
        }

        public static implicit operator int(CreditDebitIndicator creditDebitIndicator)
        {
            return creditDebitIndicator.Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
