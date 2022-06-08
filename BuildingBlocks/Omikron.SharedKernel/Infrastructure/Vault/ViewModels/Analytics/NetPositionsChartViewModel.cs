namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics
{
	public class NetPositionsChartViewModel
    {
		public int MonthIndex { get; set; }
		public GetSummaryViewModel Data { get; set; }

		public NetPositionsChartViewModel(int monthIndex, GetSummaryViewModel data)
		{
			MonthIndex = monthIndex;
			Data = data;
		}
	}
}
