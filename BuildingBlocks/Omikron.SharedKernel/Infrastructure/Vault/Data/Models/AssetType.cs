using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public class AssetType : Enumeration
    {
        private AssetType(int id, string name) : base(id, name)
        {
        }

        public static AssetType Property => new(1, "Property");
        public static AssetType Vehicle => new(2, "Vehicle");
        public static AssetType Mortgage => new(3, "Mortgage");
        public static AssetType VehicleFinance => new(4, "VehicleFinance");
        public static AssetType PersonalItem => new(5, "PersonalItem");
        public static AssetType Investment => new(6, "Investment");
        public static AssetType PersonalItemFinance => new(7, "PersonalItemFinance");
        public static AssetType Account => new AssetType(8, "Account");

        public static AssetType Parse(int value)
        {
            return value switch
            {
                1 => Property,
                2 => Vehicle,
                3 => Mortgage,
                4 => VehicleFinance,
                5 => PersonalItem,
                6 => Investment,
                7 => PersonalItemFinance,
                8 => Account,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static AssetType Parse(string value)
        {
            return value switch
            {
                "Property" => Property,
                "Properties" => Property,
                "Vehicles" => Vehicle,
                "Vehicle" => Vehicle,
                "Mortgage" => Mortgage,
                "VehicleFinance" => VehicleFinance,
                "PersonalItem" => PersonalItem,
                "PersonalItems" => PersonalItem,
                "Investment" => Investment,
                "Investments" => Investment,
                "PersonalItemFinance" => PersonalItemFinance,
                "Loan" => Account,
                "Pensions" => Account,
                "Pension" => Account,
                "CreditCard" => Account,
                "SavingsAccount" => Account,
                "Savings" => Account,
                "PrePaidCard" => Account,
                "CurrentAccount" => Account,
                "ChargeCard" => Account,
                "EMoney" => Account,
                "Account" => Account,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator AssetType(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(AssetType loanType)
        {
            return loanType.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
