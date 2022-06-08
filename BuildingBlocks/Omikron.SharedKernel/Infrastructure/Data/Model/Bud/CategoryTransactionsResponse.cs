using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class CategoryTransactionsResponse
    {

        [JsonPropertyName("categories")]
        public List<BudCategories> Categories { get; set; }
    }

    public class BudCategories
    {
        [JsonPropertyName("category")]
        public BudCategory Category { get; set; }

        [JsonPropertyName("credit_amount")]
        public List<CreditAmount> CreditAmount { get; set; }

        [JsonPropertyName("debit_amount")]
        public List<DebitAmount> DebitAmount { get; set; }
    }

    public class BudCategory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }
    }

    public class CreditAmount
    {
        [JsonPropertyName("value")]
        public float Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class DebitAmount
    {
        [JsonPropertyName("value")]
        public float Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
