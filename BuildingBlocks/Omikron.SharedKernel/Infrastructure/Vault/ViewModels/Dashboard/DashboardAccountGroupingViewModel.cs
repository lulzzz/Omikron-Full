namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard
{
	public class DashboardAccountGroupingViewModel
    {
		public DashboardAccountGroupsViewModel Assets { get; set; }
		public DashboardAccountGroupsViewModel Liabilities { get; set; }

		public DashboardAccountGroupingViewModel()
		{
			Assets = new();
			Liabilities = new();
		}
	}
}
