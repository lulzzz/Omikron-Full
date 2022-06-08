using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public class DateRange
    {
		public DateTime From { get; set; }
		public DateTime To { get; set; }

		public DateRange(DateTime from, DateTime to)
		{
			From = from;
			To = to;
		}
	}
}
