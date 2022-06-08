using Omikron.SharedKernel.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Omikron.SharedKernel.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> GetLoggerProperties(this object @object)
        {
            var properties = new Dictionary<string, string>();

            var propertiesWithLoggerField = @object.GetProperties().Where(x => x.CustomAttributes.Any());

            foreach (var property in propertiesWithLoggerField)
            {
                if (property.GetCustomAttributeByName(nameof(LoggerField)) is LoggerField loggerField)
                {
                    var propValue = @object.GetProperty(property.Name).GetValue(@object, null);
                    properties.Add(loggerField.Name, propValue?.ToString());
                }
            }

            return properties;
        }
    }
}
