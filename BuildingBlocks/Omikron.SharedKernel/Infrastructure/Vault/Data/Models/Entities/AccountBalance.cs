using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class AccountBalance : VaultItemValue<Guid>
    {
        public Guid AccountId { get; set; }
        public string BudAccountId { get; set; }
        public BalanceType BalanceType { get; set; }
		public CreditDebitIndicator CreditDebitIndicator { get; set; }

		public Account Account { get; set; }
    }
}
