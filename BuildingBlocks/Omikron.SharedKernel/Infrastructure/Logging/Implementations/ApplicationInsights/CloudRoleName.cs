using Omikron.SharedKernel.Domain;
using System.Collections.Generic;
using System.Reflection;

namespace Omikron.SharedKernel.Infrastructure.Logging.Implementations.ApplicationInsights
{
    public class CloudRoleName : ValueObject<CloudRoleName>
    {
        public string Value { get; }

        public CloudRoleName(string value)
        {
            Value = string.IsNullOrWhiteSpace(value) ? GetDefaultValue() : value;
        }

        private string GetDefaultValue()
        {
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            int index = assemblyName.LastIndexOf('.');

            return (index > 0) ? assemblyName.Substring(index + 1) : string.Empty;
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { Value };

        public static implicit operator string(CloudRoleName value)
        {
            return value.Value;
        }
    }
}
