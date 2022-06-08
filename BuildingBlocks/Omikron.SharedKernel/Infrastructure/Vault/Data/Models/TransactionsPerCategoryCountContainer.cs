namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public class TransactionsPerCategoryCountContainer
    {
		public string Category { get; set; }
		public int NumberOfSpendingTransactions { get; set; }
		public int NumberOfIncomeTransactions { get; set; }
	}
}
