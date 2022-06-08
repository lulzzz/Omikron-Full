using Omikron.SharedKernel.Utils;
using System;

namespace Omikron.SharedKernel.Extensions
{
	public static class DateTimeExtensions
	{
		public static string GetDifferenceStringFromNow(this DateTime time)
		{
			if (time > Clock.GetTime())
			{
				throw new ArgumentOutOfRangeException("Provided date is in the future");
			}

			var difference = Clock.GetTime() - time;

			if (difference.Days > 0)
			{
				if (difference.Days == 1)
				{
					return $"{difference.Days} day ago";
				}

				return $"{difference.Days} days ago";
			}

			if (difference.Hours > 0)
			{
				if (difference.Hours == 1)
				{
					return $"{difference.Hours} hour ago";
				}

				return $"{difference.Hours} hours ago";
			}

			if (difference.Minutes > 0)
			{
				if (difference.Minutes == 1)
				{
					return $"{difference.Minutes} minute ago";
				}

				return $"{difference.Minutes} minutes ago";
			}

			return "just now";
		}

		/// <summary>
		/// Get difference in months between two DateTime values
		/// </summary>
		/// <param name="higherDate">DateTime to substract from</param>
		/// <param name="lowerDate">DateTime to be substracted</param>
		/// <returns>Month difference</returns>
		public static int MonthDifference(this DateTime higherDate, DateTime lowerDate)
		{
			if (lowerDate > higherDate)
			{
				throw new ArgumentOutOfRangeException("Provided date should be lower than one extension is applied on.");
			}

			return (higherDate.Year - lowerDate.Year) * 12 + higherDate.Month - lowerDate.Month;
		}
	}
}
