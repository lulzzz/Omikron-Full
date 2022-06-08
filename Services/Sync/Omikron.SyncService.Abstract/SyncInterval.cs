using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Omikron.SharedKernel.Domain;

namespace Omikron.Sync
{
    public sealed class SyncInterval : ValueObject<SyncInterval>, IEnumerable<TimeSpan>
    {
        public SyncInterval()
        {
            Recurrence = new List<TimeSpan>();
        }

        private SyncInterval(IReadOnlyList<TimeSpan> recurrence)
        {
            Recurrence = recurrence;
        }

        public IReadOnlyList<TimeSpan> Recurrence { get; }

        protected override IEnumerable<object> EqualityCheckAttributes => Recurrence.Select(selector: span => span.To<object>());

        public IEnumerator<TimeSpan> GetEnumerator()
        {
            return Recurrence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static SyncInterval Parse(string[] values)
        {
            var occurrences = new List<TimeSpan>();

            foreach (var value in values)
            {
                if (TimeSpan.TryParse(s: value, result: out var onTime))
                {
                    occurrences.AddIfNotContains(value: onTime);
                }
            }

            return new SyncInterval(recurrence: occurrences.OrderBy(keySelector: span => span).ToList());
        }
    }
}