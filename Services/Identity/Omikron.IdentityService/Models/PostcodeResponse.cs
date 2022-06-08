using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.IdentityService.Models
{
    public class PostcodeResponse
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("addresses")]
        public List<string> Addresses { get; set; }
    }
}
