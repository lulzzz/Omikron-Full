using System;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public class AuthorizationStatus : Enumeration
    {
        public AuthorizationStatus(int id, string name) : base(id, name)
        {
        }

        public static AuthorizationStatus Valid => new(1, "Valid");
        public static AuthorizationStatus ExpiringSoon => new(2, "ExpiringSoon");
        public static AuthorizationStatus Expired => new(2, "Expired");

        public static AuthorizationStatus Parse(int value)
        {
            return value switch
            {
                1 => Valid,
                2 => ExpiringSoon,
                3 => Expired,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static AuthorizationStatus Parse(string value)
        {
            return value switch
            {
                "Valid" => Valid,
                "ExpiringSoon" => ExpiringSoon,
                "Expired" => Expired,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static AuthorizationStatus Parse(DateTime expiryDate)
        {
            var difference = expiryDate - Clock.GetTime();

            if (difference.Days < 0)
            {
                return Expired;
            }

            if (difference.Days < 7)
            {
                return ExpiringSoon;
            }

            return Valid;
        }

        public static implicit operator AuthorizationStatus(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(AuthorizationStatus authorisationStatus)
        {
            return authorisationStatus.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
