using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
	public abstract class BudBaseRequest
    {
		protected BudBaseRequest(string provider)
		{
			Provider = provider;
		}

		[JsonPropertyName("provider")]
        public string Provider { get; set; }
    }
}
