using System;
using System.Collections.Generic;
using System.Text.Json;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLogJsonPayload : ValueObject<DataChangeLogJsonPayload>
    {
        private readonly string _payload;

        private DataChangeLogJsonPayload(string payload)
        {
            _payload = payload;
        }

        public static DataChangeLogJsonPayload Parse(object @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object), "The payload object cannot be null");
            }

            var payload = JsonSerializer.Serialize(@object);
            return new DataChangeLogJsonPayload(payload);
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object>() { _payload };

        public override string ToString()
        {
            return _payload;
        }
    }
}