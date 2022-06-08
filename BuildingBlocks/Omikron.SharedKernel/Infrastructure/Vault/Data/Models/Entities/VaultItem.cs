using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class VaultItem : BaseEntity<Guid>
    {
        public CustomerId OwnerId { get; set; }
        public VaultItemType ItemType { get; set; }
        public Guid HostId { get; set; }
        public string AccountProvider { get; set; }
		public AccountSource AccountSource { get; set; }
		public AccountIdentificationNumber AccountIdentificationNumber { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }  // This needs to be updated every time HostId asset or account changes its value
		public CreditDebitIndicator CreditDebitIndicator { get; set; }
		public string ImageUrl { get; set; }
        public DateTime? AccountExpiryDate { get; set; }
        public AccountType AccountType { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
