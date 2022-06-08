using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard
{
	public class DashboardAccountGroupsViewModel
	{
		public decimal Total { get; set; }
		public string Currency { get; set; }
		public IEnumerable<GetAccountsViewModel> Items { get; set; }

		public DashboardAccountGroupsViewModel()
		{
			Total = decimal.Zero;
			Items = new List<GetAccountsViewModel>();
		}
	}
}
