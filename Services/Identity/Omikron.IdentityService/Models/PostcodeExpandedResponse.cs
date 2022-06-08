using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.IdentityService.Models
{
	public class PostcodeExpandedResponse
    {
        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("addresses")]
        public List<PostcodeAddress> Addresses { get; set; }
    }

    public class PostcodeAddress
    {
        [JsonPropertyName("formatted_address")]
        public List<string> FormattedAddress { get; set; }

        [JsonPropertyName("thoroughfare")]
        public string Thoroughfare { get; set; }

        [JsonPropertyName("building_name")]
        public string BuildingName { get; set; }

        [JsonPropertyName("sub_building_name")]
        public string SubBuildingName { get; set; }

        [JsonPropertyName("sub_building_number")]
        public string SubBuildingNumber { get; set; }

        [JsonPropertyName("building_number")]
        public string BuildingNumber { get; set; }

        [JsonPropertyName("line_1")]
        public string Line1 { get; set; }

        [JsonPropertyName("line_2")]
        public string Line2 { get; set; }

        [JsonPropertyName("line_3")]
        public string Line3 { get; set; }

        [JsonPropertyName("line_4")]
        public string Line4 { get; set; }

        [JsonPropertyName("locality")]
        public string Locality { get; set; }

        [JsonPropertyName("town_or_city")]
        public string TownOrCity { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("district")]
        public string District { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
