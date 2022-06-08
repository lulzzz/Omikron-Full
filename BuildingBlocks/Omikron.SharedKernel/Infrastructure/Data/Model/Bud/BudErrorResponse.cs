using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class BudErrorResponse
    {
        [JsonPropertyName("operation_id")]
        public string OperationId { get; set; }

        [JsonPropertyName("code_id")]
        public string CodeId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
