using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class BudListAccountsResponse
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("account_type")]
        public string AccountType { get; set; }

        [JsonPropertyName("account_sub_type")]
        public string AccountSubType { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("details")]
        public List<BudDetail> Details { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("balances")]
        public List<BudBalance> Balances { get; set; }
    }

    public class BudBalance
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("amount")]
        public BudAmount Amount { get; set; }

        [JsonPropertyName("credit_debit_indicator")]
        public string CreditDebitIndicator { get; set; }

        [JsonPropertyName("credit_line")]
        public List<BudCreditLine> CreditLine { get; set; }

        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BudCreditLine
    {
        [JsonPropertyName("included")]
        public bool Included { get; set; }

        [JsonPropertyName("amount")]
        public BudAmount Amount { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BudAmount
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class BudDetail
    {
        [JsonPropertyName("scheme_name")]
        public string SchemeName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("secondary_identification")]
        public string SecondaryIdentification { get; set; }
    }

    public class ListAccountsMetadata
    {
        [JsonPropertyName("results")]
        public int Results { get; set; }
    }
}
