using Omikron.SharedKernel.Domain;
using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public class TransactionStatus : Enumeration
    {
        private TransactionStatus(int id, string name) : base(id, name)
        {
        }

        public static TransactionStatus Booked => new(1, "Booked");
        public static TransactionStatus Pending => new(2, "Pending");

        public static TransactionStatus Parse(int value)
        {
            return value switch
            {
                1 => Booked,
                2 => Pending,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static TransactionStatus Parse(string value)
        {
            return value switch
            {
                "Booked" => Booked,
                "Pending" => Pending,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator TransactionStatus(string value)
        {
            return Parse(value);
        }

        public static implicit operator string(TransactionStatus transactionStatus)
        {
            return transactionStatus.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
