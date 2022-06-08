using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
	public class TransactionViewModelContainer
    {
		public string Date { get; set; }
		public IEnumerable<TransactionViewModel> Transactions { get; set; }
	}
}
