using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
	public class BudInitiateKycResponse
    {
		[JsonPropertyName("check_uuid")]
		public string CheckUuid { get; set; }
	}
}
