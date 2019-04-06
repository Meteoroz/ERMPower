using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class MedianExtensions
    {
        public static decimal GetMedian(this IEnumerable<decimal> source)
        {
            int decimals = source.Count();
            if (decimals != 0)
            {
                var midpoint = (decimals - 1) / 2;
                var sorted = source.OrderBy(n => n);
                var median = sorted.ElementAt(midpoint);
                if (decimals % 2 == 0)
                {
                    median = (median + sorted.ElementAt(midpoint + 1)) / 2;
                }

                return median;
            }

            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static bool IsOutOfRangeOfMedian(this decimal source, decimal median, decimal percentage)
        {
            var result = false;
            var upper = median + (median * percentage);
            var lower = median - (median * percentage);
            if (source > upper || source < lower)
            {
                result = true;
            }

            return result;
        }
    }
}
