using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class RetrieveRevokeConsentStatusMetadataResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
