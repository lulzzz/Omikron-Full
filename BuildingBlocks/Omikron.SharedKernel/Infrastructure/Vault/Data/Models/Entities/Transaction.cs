using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class Transaction : BaseEntity<Guid>
    {
        public Guid AccountId { get; set; }
        public Guid? MerchantId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
		public string TransactionInformation { get; set; }
		public CreditDebitIndicator CreditDebitIndicator { get; set; }
		public TransactionStatus TransactionStatus { get; set; }
		public string BudTransactionId { get; set; }

		public Account Account { get; set; }
		public Merchant Merchant { get; set; }
	}
}
