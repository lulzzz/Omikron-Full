using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public class LoanType : Enumeration
    {
        private LoanType(int id, string name) : base(id, name)
        {
        }

        public static LoanType Mortgage => new(1, "Mortgage");
        public static LoanType FinancialAgreement => new(2, "FinancialAgreement");

        public static LoanType Parse(int value)
        {
            return value switch
            {
                1 => Mortgage,
                2 => FinancialAgreement,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static LoanType Parse(string value)
        {
            return value switch
            {
                "Mortgage" => Mortgage,
                "FinancialAgreement" => FinancialAgreement,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator LoanType(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(LoanType loanType)
        {
            return loanType.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
