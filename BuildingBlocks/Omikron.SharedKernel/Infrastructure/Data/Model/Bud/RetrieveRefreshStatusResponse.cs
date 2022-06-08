using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class RetrieveRefreshStatusResponse
    {
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("reconnect_required")]
        public bool ReconnectRequired { get; set; }

        [JsonPropertyName("step")]
        public int Step { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
