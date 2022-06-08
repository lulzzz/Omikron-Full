using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class BudRetrieveMerchantTotalsResponse
    {
        [JsonPropertyName("total_outgoing")]
        public BudRetrieveMerchantTotalsTotalOutgoing TotalOutgoing { get; set; }

        [JsonPropertyName("total_incoming")]
        public BudRetrieveMerchantTotalsTotalIncoming TotalIncoming { get; set; }

        [JsonPropertyName("unknown_spend")]
        public BudRetrieveMerchantTotalsUnknownSpend UnknownSpend { get; set; }

        [JsonPropertyName("merchant_spend")]
        public Dictionary<string, BudRetrieveMerchantTotalsMerchantDetails> MerchantSpend { get; set; }
    }

    public class BudRetrieveMerchantTotalsTotalOutgoing
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class BudRetrieveMerchantTotalsTotalIncoming
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class BudRetrieveMerchantTotalsUnknownSpend
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("transactions")]
        public int Transactions { get; set; }
    }

    public class BudRetrieveMerchantTotalsMerchantDetails
	{
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("transactions")]
        public int Transactions { get; set; }
    }
}
