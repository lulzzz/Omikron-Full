using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics
{
	public class MerchantContainerViewModel
    {
		public int NumberOfMerchants { get; set; }
		public decimal TotalValue { get; set; }
		public string Currency { get; set; }
		public IEnumerable<MerchantViewModel> Merchants { get; set; }
	}
}
