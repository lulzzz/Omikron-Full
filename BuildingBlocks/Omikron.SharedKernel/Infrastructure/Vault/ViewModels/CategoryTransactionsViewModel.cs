using System.Collections.Generic;

namespace Omikron.VaultService.ViewModels
{
    public class CategoryTransactionsViewModel
    {
        public List<CategoryViewModel> Expenditure { get; set; }
        public List<CategoryViewModel> Income { get; set; }
        public decimal TotalExpenditure { get; set; }
        public decimal TotalIncome{ get; set; }
        public int TotalNumberOfExpenditureTransactions{ get; set; }
        public int TotalNumberOfIncomeTransactions{ get; set; }

		public CategoryTransactionsViewModel()
		{
            Expenditure = new();
            Income = new();
		}
    }

    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfTransactions { get; set; }
        public string Icon { get; set; }
    }

    public class CategoryData
	{
        public string CategoryName { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public int NumberOfCreditTransactions { get; set; }
        public int NumberOfDebitTransactions { get; set; }
	}
}
