using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
namespace AdventureWorksLT2019.MauiXApp.Common.Helpers;

public class SelectListHelper
{
    public static List<NameValuePair> GetDefaultPredefinedDateTimeRange(bool past = true, bool future = false)
    {

        var result = new List<NameValuePair>(new[] {
            new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_AllTime, Value = PreDefinedDateTimeRanges.AllTime.ToString() },
            new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_Custom, Value = PreDefinedDateTimeRanges.Custom.ToString() } });

        if (future)
        {
            result.AddRange(new[] {
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextTenYears, Value = PreDefinedDateTimeRanges.NextTenYears.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextFiveYears, Value = PreDefinedDateTimeRanges.NextFiveYears.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextYear, Value = PreDefinedDateTimeRanges.NextYear.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextSixMonths, Value = PreDefinedDateTimeRanges.NextSixMonths.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextThreeMonths, Value = PreDefinedDateTimeRanges.NextThreeMonths.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextMonth, Value = PreDefinedDateTimeRanges.NextMonth.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_NextWeek, Value = PreDefinedDateTimeRanges.NextWeek.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_Tomorrow, Value = PreDefinedDateTimeRanges.Tomorrow.ToString() },
            });
        }

        result.AddRange(new[] {
            new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_ThisYear, Value = PreDefinedDateTimeRanges.ThisYear.ToString() },
            new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_ThisMonth, Value = PreDefinedDateTimeRanges.ThisMonth.ToString() },
            new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_ThisWeek, Value = PreDefinedDateTimeRanges.ThisWeek.ToString() },
            new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_Today, Value = PreDefinedDateTimeRanges.Today.ToString() },
            });

        if (past)
        {
            result.AddRange(new[] {
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_Yesterday, Value = PreDefinedDateTimeRanges.Yesterday.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastWeek, Value = PreDefinedDateTimeRanges.LastWeek.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastMonth, Value = PreDefinedDateTimeRanges.LastMonth.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastThreeMonths, Value = PreDefinedDateTimeRanges.LastThreeMonths.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastSixMonths, Value = PreDefinedDateTimeRanges.LastSixMonths.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastYear, Value = PreDefinedDateTimeRanges.LastYear.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastFiveYears, Value = PreDefinedDateTimeRanges.LastFiveYears.ToString() },
                new NameValuePair { Name = UIStrings.PreDefinedDateTimeRanges_LastTenYears, Value = PreDefinedDateTimeRanges.LastTenYears.ToString() },
            });
        }

        return result;
    }
}

