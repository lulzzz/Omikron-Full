using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class VaultViewModel
    {
        public IEnumerable<AccountGroupViewModel> Accounts { get; set; }
        public IEnumerable<AssetGruopViewModel> Assets { get; set; }

    }
}
