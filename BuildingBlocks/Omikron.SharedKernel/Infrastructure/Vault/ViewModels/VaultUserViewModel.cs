using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class VaultUserViewModel
    {
		public Guid Id { get; set; }
		public string BudCustomerId { get; set; }
        public string BudCustomerSecret { get; set; }
    }
}
