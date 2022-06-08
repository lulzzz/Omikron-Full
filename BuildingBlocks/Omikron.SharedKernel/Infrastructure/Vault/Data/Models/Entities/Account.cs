using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public sealed class Account : BaseVaultItem<Guid>
    {
        public AccountSource Source { get; set; }
        public AccountType Type { get; set; }
        public string Provider { get; set; }
        public AccountIdentificationNumber IdentificationNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public LoanType LoanType { get; set; }
        public string Notes { get; set; }
        public string ReferenceNumber  { get; set; }
		public string BudAccountId { get; set; }

		public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<PersonalItem> PersonalItems { get; set; }
        public IEnumerable<AccountBalance> AccountBalances { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}