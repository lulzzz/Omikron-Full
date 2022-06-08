using Omikron.SharedKernel.Domain;
using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
	public class Merchant : BaseEntity<Guid>
    {
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Logo { get; set; }

		public IEnumerable<Transaction> Transactions { get; set; }
	}
}
