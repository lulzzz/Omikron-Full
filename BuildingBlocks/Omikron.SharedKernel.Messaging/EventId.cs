using System.Collections.Generic;

namespace Omikron.SharedKernel.Messaging
{
    public abstract class EventId
    {
        protected abstract IEnumerable<object> EqualityCheckAttributes { get; }

        public virtual string Id()
        {
            var id = string.Join(separator: null, values: EqualityCheckAttributes).Replace(oldValue: "-", newValue: string.Empty);
            id = $"{GetType().Name}-{id}";

            if (id.Length > 128)
            {
                id = id.Substring(startIndex: 0, length: 128);
            }

            return id;
        }
    }
}