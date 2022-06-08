using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class VaultItemValueViewModel
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
    }
}