using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics
{
	public class VaultEntryListViewModel
    {
		public string GroupName { get; set; }
		public IEnumerable<VaultEntryViewModel> VaultEntries { get; set; }
	}
}
