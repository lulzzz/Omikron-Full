using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
	public class BudRetrieveKYCCheckResponse
    {
        [JsonPropertyName("alerts")]
        public List<BudAlert> Alerts { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("center_location")]
        public string CenterLocation { get; set; }

        [JsonPropertyName("client_name")]
        public string ClientName { get; set; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("document_count")]
        public int DocumentCount { get; set; }

        [JsonPropertyName("org_uid")]
        public string OrgUid { get; set; }

        [JsonPropertyName("profile")]
        public BudProfile Profile { get; set; }

        [JsonPropertyName("rag")]
        public string Rag { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("suspicious")]
        public bool Suspicious { get; set; }

        [JsonPropertyName("transaction_reference")]
        public string TransactionReference { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
    }

    public class BudAlert
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("crid")]
        public string Crid { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BudProfile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
    }
}
