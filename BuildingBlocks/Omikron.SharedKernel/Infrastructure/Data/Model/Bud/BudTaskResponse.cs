using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
    public class BudTaskResponse
    {
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }
    }
}
