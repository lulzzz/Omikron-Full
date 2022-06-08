using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class BudBaseResponse<T>
    {
        [JsonPropertyName("operation_id")]
        public string OperationId { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }
    }

    public class BudMetadataResponse<T>
    {
        [JsonPropertyName("operation_id")]
        public string OperationId { get; set; }

        [JsonPropertyName("metadata")]
        public T Metadata { get; set; }
    }

    public class BudBaseResponse<TData, TMetadata>
    {
        [JsonPropertyName("operation_id")]
        public string OperationId { get; set; }

        [JsonPropertyName("data")]
        public TData Data { get; set; }

        [JsonPropertyName("metadata")]
        public TMetadata Metadata { get; set; }
    }
}
