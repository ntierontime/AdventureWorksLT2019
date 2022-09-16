namespace AdventureWorksLT2019.MauiXApp.Common.Helpers;

public class SelectListHelper
{
    public static List<Framework.Models.NameValuePair> GetDefaultPredefinedDateTimeRange(bool past = true, bool future = false)
    {
        
        var result = new List<Framework.Models.NameValuePair>(new[] {
            new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_AllTime, Value = Framework.Models.PreDefinedDateTimeRanges.AllTime.ToString() },
            new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_Custom, Value = Framework.Models.PreDefinedDateTimeRanges.Custom.ToString() } });

        if (future)
        {
            result.AddRange(new[] {
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextTenYears, Value = Framework.Models.PreDefinedDateTimeRanges.NextTenYears.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextFiveYears, Value = Framework.Models.PreDefinedDateTimeRanges.NextFiveYears.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextYear, Value = Framework.Models.PreDefinedDateTimeRanges.NextYear.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextSixMonths, Value = Framework.Models.PreDefinedDateTimeRanges.NextSixMonths.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextThreeMonths, Value = Framework.Models.PreDefinedDateTimeRanges.NextThreeMonths.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextMonth, Value = Framework.Models.PreDefinedDateTimeRanges.NextMonth.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_NextWeek, Value = Framework.Models.PreDefinedDateTimeRanges.NextWeek.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_Tomorrow, Value = Framework.Models.PreDefinedDateTimeRanges.Tomorrow.ToString() },
            });
        }

        result.AddRange(new[] {
            new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_ThisYear, Value = Framework.Models.PreDefinedDateTimeRanges.ThisYear.ToString() },
            new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_ThisMonth, Value = Framework.Models.PreDefinedDateTimeRanges.ThisMonth.ToString() },
            new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_ThisWeek, Value = Framework.Models.PreDefinedDateTimeRanges.ThisWeek.ToString() },
            new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_Today, Value = Framework.Models.PreDefinedDateTimeRanges.Today.ToString() },
            });

        if (past)
        {
            result.AddRange(new[] {
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_Yesterday, Value = Framework.Models.PreDefinedDateTimeRanges.Yesterday.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastWeek, Value = Framework.Models.PreDefinedDateTimeRanges.LastWeek.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastMonth, Value = Framework.Models.PreDefinedDateTimeRanges.LastMonth.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastThreeMonths, Value = Framework.Models.PreDefinedDateTimeRanges.LastThreeMonths.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastSixMonths, Value = Framework.Models.PreDefinedDateTimeRanges.LastSixMonths.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastYear, Value = Framework.Models.PreDefinedDateTimeRanges.LastYear.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastFiveYears, Value = Framework.Models.PreDefinedDateTimeRanges.LastFiveYears.ToString() },
                new Framework.Models.NameValuePair { Name = AdventureWorksLT2019.Resx.Resources.UIStrings.PreDefinedDateTimeRanges_LastTenYears, Value = Framework.Models.PreDefinedDateTimeRanges.LastTenYears.ToString() },
            });
        }

        return result;
    }
}
