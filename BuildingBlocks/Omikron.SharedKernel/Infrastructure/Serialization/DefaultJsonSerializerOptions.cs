using System.Text.Json;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Json
{
    public class DefaultJsonSerializerOptions
    {
        public static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        static DefaultJsonSerializerOptions()
        {
            AddJsonConverters();

            DefaultSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            DefaultSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        private static void AddJsonConverters()
        {
            // Add converters
        }

        public static void Init()
        {
            /*Note: System.Text.Json does not have global JsonSerializerOptions for JsonSerializer 
             * as Newtonsoft.Json.JsonConvert does e.g. JsonConvert.DefaultSettings = () => DefaultSerializerSettings;
             */
        }
    }
}