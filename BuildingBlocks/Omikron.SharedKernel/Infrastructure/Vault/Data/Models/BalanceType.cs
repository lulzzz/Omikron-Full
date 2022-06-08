using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public class BalanceType : Enumeration
    {
        public BalanceType(int id, string name) : base(id, name)
        {
        }

        public static BalanceType ClosingAvailable => new(1, "ClosingAvailable");
        public static BalanceType ClosingBooked => new(2, "ClosingBooked");
        public static BalanceType ClosingCleared => new(3, "ClosingCleared");
        public static BalanceType Expected => new(4, "Expected");
        public static BalanceType ForwardAvailable => new(5, "ForwardAvailable");
        public static BalanceType Information => new(6, "Information");
        public static BalanceType InterimAvailable => new(7, "InterimAvailable");
        public static BalanceType InterimBooked => new(8, "InterimBooked");
        public static BalanceType InterimCleared => new(9, "InterimCleared");
        public static BalanceType OpeningAvailable => new(10, "OpeningAvailable");
        public static BalanceType OpeningBooked => new(11, "OpeningBooked");
        public static BalanceType OpeningCleared => new(12, "OpeningCleared");
        public static BalanceType PreviouslyClosedBooked => new(13, "PreviouslyClosedBooked");

        public static BalanceType Parse(int value)
        {
            return value switch
            {
                1 => ClosingAvailable,
                2 => ClosingBooked,
                3 => ClosingCleared,
                4 => Expected,
                5 => ForwardAvailable,
                6 => Information,
                7 => InterimAvailable,
                8 => InterimBooked,
                9 => InterimCleared,
                10 => OpeningAvailable,
                11 => OpeningBooked,
                12 => OpeningCleared,
                13 => PreviouslyClosedBooked,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static BalanceType Parse(string value)
        {
            return value switch
            {
                "ClosingAvailable" => ClosingAvailable,
                "ClosingBooked" => ClosingBooked,
                "ClosingCleared" => ClosingCleared,
                "Expected" => Expected,
                "ForwardAvailable" => ForwardAvailable,
                "Information" => Information,
                "InterimAvailable" => InterimAvailable,
                "InterimBooked" => InterimBooked,
                "InterimCleared" => InterimCleared,
                "OpeningAvailable" => OpeningAvailable,
                "OpeningBooked" => OpeningBooked,
                "OpeningCleared" => OpeningCleared,
                "PreviouslyClosedBooked" => PreviouslyClosedBooked,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static implicit operator BalanceType(string value)
        {
            return Parse(value);
        }
    }
}
