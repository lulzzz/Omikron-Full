using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
	public class LoanViewModel
	{
		public LoanViewModel(Guid id, string name, decimal balance)
		{
			Id = id;
			Name = name;
			Balance = balance;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Balance { get; set; }
	}
}
