namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class GetSummaryViewModel
    {
        public decimal Assets { get; set; }
        public decimal Liabilities { get; set; }
        public decimal Net => Liabilities <= 0 ? Assets + Liabilities : Assets - Liabilities;

        public GetSummaryViewModel() {}

        public GetSummaryViewModel(decimal assets, decimal liabilities)
        {
            Assets = assets;
            Liabilities = liabilities;
        }

        public static GetSummaryViewModel operator +(GetSummaryViewModel a, GetSummaryViewModel b)
        => new GetSummaryViewModel(a.Assets + b.Assets, a.Liabilities + b.Liabilities);
    }
}
