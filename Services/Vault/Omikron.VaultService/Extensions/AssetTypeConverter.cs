using System;
using System.Text.Json;
using Omikron.SharedKernel.Convertors;
using Omikron.SharedKernel.Domain.Reporting;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.VaultService.Extensions
{
    public class AssetTypeConverter : EnumerationConverter<AssetType>
    {
        public override AssetType Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
        {
            return AssetType.Parse(value: reader.GetString());
        }
    }
}