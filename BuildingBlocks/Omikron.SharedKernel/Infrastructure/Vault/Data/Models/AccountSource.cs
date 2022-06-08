using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public sealed class AccountSource : Enumeration
    {
        public AccountSource()
        {
        }

        public AccountSource(int id, string name) : base(id: id, name: name)
        {
        }

        public static AccountSource Manual => new AccountSource(id: 1, name: "Manual");
        public static AccountSource BudApi => new AccountSource(id: 2, name: "Bud API");

        public static AccountSource Parse(int value)
        {
            return value switch
            {
                1 => Manual,
                2 => BudApi,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value))
            };
        }

        public static AccountSource Parse(string value)
        {
            return value switch
            {
                "Manual" => Manual,
                "Bud API" => BudApi,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value))
            };
        }

        public static implicit operator AccountSource(int value)
        {
            return Parse(value: value);
        }

        public static implicit operator int(AccountSource value)
        {
            return value.Id;
        }

        public static explicit operator AccountSource(string value)
        {
            return Parse(value: value);
        }

        public static implicit operator string(AccountSource accountType)
        {
            return accountType.ToString();
        }
    }
}