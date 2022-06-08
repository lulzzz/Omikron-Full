using Omikron.SharedKernel.Domain;
using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	//TODO: AccountTypes which Bud supports and we don't should be moved here and out AccountType enumeration should contain only out internal account types
	public class BudSpecificAccountType : Enumeration
    {
        private BudSpecificAccountType(int id, string name) : base(id, name)
        {
        }

        public static BudSpecificAccountType Mortgage => new(1, "Mortgage");

        public static BudSpecificAccountType Parse(int value)
        {
            return value switch
            {
                1 => Mortgage,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static BudSpecificAccountType Parse(string value)
        {
            return value switch
            {
                "Mortgage" => Mortgage,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator BudSpecificAccountType(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(BudSpecificAccountType loanType)
        {
            return loanType.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
