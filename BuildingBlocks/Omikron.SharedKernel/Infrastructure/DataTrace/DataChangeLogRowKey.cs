using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLogRowKey : ValueObject<DataChangeLogRowKey>
    {
        private readonly DateTime _executedAtUtc;

        private DataChangeLogRowKey(DateTime executedAtUtc)
        {
            _executedAtUtc = executedAtUtc;
        }

        public static DataChangeLogRowKey Parse(DateTime executedAtUtc)
        {
            if (executedAtUtc == default(DateTime))
            {
                throw new ArgumentException("The execution time cannot be empty.");
            }

            return new DataChangeLogRowKey(executedAtUtc);
        }

        public override string ToString()
        {
            return $"{_executedAtUtc.Ticks:d16}";
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object>() { _executedAtUtc };
    }
}