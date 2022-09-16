using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    /// <summary>
    /// PreDefine dDateTime Ranges
    /// </summary>
    public static class PreDefinedDateTimeRangesHelper
    {
        public static DateTime? GetLowerBound(string preDefinedDateTimeRange)
        {
            if(Enum.TryParse<Framework.Models.PreDefinedDateTimeRanges>(preDefinedDateTimeRange, out var preDefinedDateTimeRangeEnum))
            {
                return GetLowerBound(preDefinedDateTimeRangeEnum);
            }
            return GetLowerBound(PreDefinedDateTimeRanges.AllTime);
        }
        public static DateTime? GetLowerBound(Framework.Models.PreDefinedDateTimeRanges preDefinedDateTimeRange)
        {
            if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.AllTime)
            {
                return null;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Custom)
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastTenYears)
            // include this year + 10 years
            {
                var lowerBound = DateTime.Now.AddYears(-10);
                return new DateTime(lowerBound.Year, 1, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastFiveYears)
            // include this year + 5 years
            {
                var lowerBound = DateTime.Now.AddYears(-5);
                return new DateTime(lowerBound.Year, 1, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastYear)
            // include this year + 1 year
            {
                var lowerBound = DateTime.Now.AddYears(-1);
                return new DateTime(lowerBound.Year, 1, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastSixMonths)
            // include this month + 6 month
            {
                var lowerBound = DateTime.Now.AddMonths(-6);
                return new DateTime(lowerBound.Year, lowerBound.Month, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastThreeMonths)
            // include this month + 3 month
            {
                var lowerBound = DateTime.Now.AddMonths(-3);
                return new DateTime(lowerBound.Year, lowerBound.Month, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastMonth)
            // include this month + 1 month
            {
                var lowerBound = DateTime.Now.AddMonths(-1);
                return new DateTime(lowerBound.Year, lowerBound.Month, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastWeek)
            // include this week + 1 week
            {
                var lowerBound = DateTime.Now.AddDays(-7);
                lowerBound = lowerBound.AddDays(-(int)lowerBound.DayOfWeek);// starting from Sunday
                return new DateTime(lowerBound.Year, lowerBound.Month, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Yesterday)
            // exclude today
            {
                var lowerBound = DateTime.Now.AddDays(-1);
                return new DateTime(lowerBound.Year, lowerBound.Month, lowerBound.Day, 0, 0, 0);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.ThisYear)
            {
                return new DateTime(DateTime.Now.Year, 1, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.ThisMonth)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.ThisWeek)
            {
                return DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Today)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Tomorrow)
            // exclude today
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextWeek)
            // exclude this week
            {
                return DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).AddDays(7);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextMonth)
            // exclude this month
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextThreeMonths)
            // exclude this month
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextSixMonths)
            // exclude this month
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextYear)
            // exclude this year
            {
                return new DateTime(DateTime.Now.Year, 1, 1).AddYears(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextFiveYears)
            // exclude this year
            {
                return new DateTime(DateTime.Now.Year, 1, 1).AddYears(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextTenYears)
            // exclude this year
            {
                return new DateTime(DateTime.Now.Year, 1, 1).AddYears(1);
            }

            return null;
        }

        public static DateTime? GetUpperBound(string preDefinedDateTimeRange)
        {
            if (Enum.TryParse<Framework.Models.PreDefinedDateTimeRanges>(preDefinedDateTimeRange, out var preDefinedDateTimeRangeEnum))
            {
                return GetUpperBound(preDefinedDateTimeRangeEnum);
            }
            return GetUpperBound(PreDefinedDateTimeRanges.AllTime);
        }
        public static DateTime? GetUpperBound(Framework.Models.PreDefinedDateTimeRanges preDefinedDateTimeRange)
        {
            if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.AllTime)
            {
                return null;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Custom)
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastTenYears)
            // include this year + 10 years
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastFiveYears)
            // include this year + 5 years
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastYear)
            // include this year + 1 year
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastSixMonths)
            // include this month + 6 month
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastThreeMonths)
            // include this month + 3 month
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastMonth)
            // include this month + 1 month
            {
                return DateTime.Now;
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.LastWeek)
            // include this week + 1 week
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddDays(7);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Yesterday)
            // exclude today
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddDays(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.ThisYear)
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddYears(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.ThisMonth)
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddMonths(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.ThisWeek)
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddDays(7);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Today)
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddDays(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.Tomorrow)
            // exclude today
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddDays(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextWeek)
            // exclude this week
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddDays(7);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextMonth)
            // exclude this month
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddMonths(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextThreeMonths)
            // exclude this month
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddMonths(3);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextSixMonths)
            // exclude this month
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddMonths(6);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextYear)
            // exclude this year
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddYears(1);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextFiveYears)
            // exclude this year
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddYears(5);
            }
            else if (preDefinedDateTimeRange == Framework.Models.PreDefinedDateTimeRanges.NextTenYears)
            // exclude this year
            {
                var lowerBound = GetLowerBound(preDefinedDateTimeRange);
                return lowerBound?.AddYears(10);
            }

            return null;
        }
    }
}

