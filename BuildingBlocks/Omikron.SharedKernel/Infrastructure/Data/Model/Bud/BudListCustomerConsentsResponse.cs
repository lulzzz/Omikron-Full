using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
	public class BudListCustomerConsentsResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("transaction_from_date")]
        public DateTime TransactionFromDate { get; set; }

        [JsonPropertyName("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("last_consented_on")]
        public DateTime LastConsentedOn { get; set; }

        [JsonPropertyName("permissions")]
        public List<string> Permissions { get; set; }

        [JsonPropertyName("raw_version")]
        public string RawVersion { get; set; }

        [JsonPropertyName("raw_format")]
        public string RawFormat { get; set; }

        [JsonPropertyName("raw")]
        public BudConsentsRaw Raw { get; set; }

        [JsonPropertyName("account_ids")]
        public List<string> AccountIds { get; set; }

        [JsonPropertyName("last_synced_at")]
        public DateTime LastSyncedAt { get; set; }
    }

    public class BudConsentsRaw
    {
        [JsonPropertyName("ConsentId")]
        public string ConsentId { get; set; }

        [JsonPropertyName("TransactionFromDateTime")]
        public object TransactionFromDateTime { get; set; }

        [JsonPropertyName("StatusUpdateDateTime")]
        public object StatusUpdateDateTime { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("Permissions")]
        public List<string> Permissions { get; set; }
    }
}
