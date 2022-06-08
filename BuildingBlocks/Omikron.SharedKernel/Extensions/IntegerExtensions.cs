using System;

namespace Omikron.SharedKernel.Extensions
{
	public static class IntegerExtensions
    {
        public static decimal TrendOverRange(this int range, decimal startingPoint, decimal endingPoint)
		{
            if (range == 0)
			{
                throw new ArgumentOutOfRangeException("Range has to be greater than zero.");
			}

            return Math.Abs(startingPoint - endingPoint) / range;
        }
    }
}
