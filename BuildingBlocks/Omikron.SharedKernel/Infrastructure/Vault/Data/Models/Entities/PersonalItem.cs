using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
	public class PersonalItem : BaseVaultItem<Guid>
    {
		public Guid? FinancialAgreementId { get; set; }
		public Account FinancialAgreement { get; set; }
		public IEnumerable<PersonalItemValue> PersonalItemValues { get; set; }
	}
}