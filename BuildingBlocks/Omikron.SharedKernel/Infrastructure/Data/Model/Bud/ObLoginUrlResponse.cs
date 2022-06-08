using System;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class ObLoginUrlResponse
    {
        [JsonPropertyName("url")]
        public Uri Url { get; set; }
    }
}
