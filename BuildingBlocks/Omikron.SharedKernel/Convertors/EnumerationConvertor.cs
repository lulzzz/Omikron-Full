using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Convertors
{
    public abstract class EnumerationConverter<TEnumeration> : JsonConverter<TEnumeration> where TEnumeration : Enumeration
    {
        public override void Write(Utf8JsonWriter writer, TEnumeration value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        public override TEnumeration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Please make implementation of specific enumeration for read.");
        }
    }
}