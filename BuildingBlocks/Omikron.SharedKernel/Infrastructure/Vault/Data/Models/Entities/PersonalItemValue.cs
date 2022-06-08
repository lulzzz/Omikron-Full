using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
	public class PersonalItemValue : VaultItemValue<Guid>
    {
		public Guid PersonalItemId { get; set; }

		public PersonalItem PersonalItem { get; set; }
	}
}
