using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class ObLoginRequest
    {
        [JsonPropertyName("redirect_url")]
        public Uri RequestUrl { get; set; }

        [JsonPropertyName("providers")]
        public IEnumerable<string> Providers { get; set; }
    }
}
