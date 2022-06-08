using System.Text.Json;

namespace Omikron.VaultService.Extensions
{
    public static class JsonConverterExtensions
    {
        public static void AddJsonConverters(this JsonSerializerOptions settings)
        {
            settings.Converters.Add(new AssetTypeConverter());
        }
    }
}