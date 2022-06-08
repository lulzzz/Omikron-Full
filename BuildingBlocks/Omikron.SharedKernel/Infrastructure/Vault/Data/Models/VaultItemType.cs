using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Vault.Extensions;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public class VaultItemType : Enumeration
    {
        private VaultItemType(int id, string name) : base(id, name)
        {
        }

        public static VaultItemType Account => new(1, "Account");
        public static VaultItemType Investment => new(2, "Investment");
        public static VaultItemType Property => new(3, "Property");
        public static VaultItemType Vehicle => new(4, "Vehicle");
        public static VaultItemType PersonalItem => new(5, "PersonalItem");

        public static VaultItemType Parse(int value)
        {
            return value switch
            {
                1 => Account,
                2 => Investment,
                3 => Property,
                4 => Vehicle,
                5 => PersonalItem,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static VaultItemType Parse(string value)
        {
            return value switch
            {
                "Account" => Account,
                "Property" => Property,
                "Properties" => Property,
                "Vehicle" => Vehicle,
                "Vehicles" => Vehicle,
                "PersonalItem" => PersonalItem,
                "Personal Items" => PersonalItem,
                "Investment" => Investment,
                "Investments" => Investment,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator VaultItemType(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(VaultItemType vaultItemType)
        {
            return vaultItemType.ToString();
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<string> EnumerateTypesAsDisplayNames()
        {
            return new List<string>()
            {
                Property.ToString().ToAccountGroupDisplayName(),
                Vehicle.ToString().ToAccountGroupDisplayName(),
                PersonalItem.ToString().ToAccountGroupDisplayName(),
                Investment.ToString().ToAccountGroupDisplayName(),
            };
        }
    }
}
