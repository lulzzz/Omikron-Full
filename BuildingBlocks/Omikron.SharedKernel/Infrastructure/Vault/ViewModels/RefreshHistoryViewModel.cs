namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class RefreshHistoryViewModel
    {
        public RefreshHistoryViewModel(string lastRefresh)
        {
            LastRefresh = lastRefresh;
        }

        public string LastRefresh { get; }
    }
}
