using System.Collections.Generic;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class AccountGroupViewModel
    {
        public string AccountTypes { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}