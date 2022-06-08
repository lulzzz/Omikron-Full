using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard
{
	public class DashboardChartViewModel
	{
		public string DateIndex { get; set; }
		public GetSummaryViewModel Data { get; set; }

		public DashboardChartViewModel(string dateIndex, GetSummaryViewModel data)
		{
			DateIndex = dateIndex;
			Data = data;
		}

		public static DashboardChartViewModel operator +(DashboardChartViewModel a, DashboardChartViewModel b)
		=> new DashboardChartViewModel(a.DateIndex, a.Data + b.Data);
	}
}
