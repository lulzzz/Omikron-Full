using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class ObProvidersResponse
    {
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("maintenance_window")]
        public ObProviderMaintenanceWindow MaintenanceWindow { get; set; }

        [JsonPropertyName("maintenance_status")]
        public string MaintenanceStatus { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("regions")]
        public List<string> Regions { get; set; }
    }

    public class ObProviderMaintenanceWindow
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }
    }
}
