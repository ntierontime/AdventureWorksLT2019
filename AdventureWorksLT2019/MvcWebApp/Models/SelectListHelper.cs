using AdventureWorksLT2019.Resx;
using Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class SelectListHelper
    {
        private readonly IUIStrings _localizor;

        public SelectListHelper(IUIStrings localizor)
        {
            _localizor = localizor;
        }

        public List<NameValuePair> GetDefaultTrueFalseBooleanSelectList()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair{ Name = _localizor.Get("All"), Value = "", Selected=true },
                new NameValuePair{ Name = _localizor.Get("True"), Value = "True"  },
                new NameValuePair{ Name = _localizor.Get("False"), Value = "False"  },
            });
        }

        public List<NameValuePair> GetTextSearchTypeList()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair{ Name = _localizor.Get(TextSearchTypes.Contains.ToString()), Value = "", Selected=true },
                new NameValuePair{ Name = _localizor.Get(TextSearchTypes.StartsWith.ToString()), Value = TextSearchTypes.StartsWith.ToString() },
                new NameValuePair{ Name = _localizor.Get(TextSearchTypes.EndsWith.ToString()), Value = TextSearchTypes.EndsWith.ToString()},
            });
        }

        public List<NameValuePair> GetDefaultPageSizeList()
        {
            var format_ItemsPerPage = _localizor.Get("Format_ItemsPerPage");

            return new List<NameValuePair>(new[] {
                new NameValuePair{ Name = string.Format(format_ItemsPerPage, 10), Value = "10", Selected=true },
                new NameValuePair{ Name = string.Format(format_ItemsPerPage, 25), Value = "25" },
                new NameValuePair{ Name = string.Format(format_ItemsPerPage, 50), Value = "50" },
                new NameValuePair{ Name = string.Format(format_ItemsPerPage, 100), Value = "100" },
            });
        }

        public List<NameValuePair> GetDefaultPredefinedDateTimeRange(bool past = true, bool future = false)
        {
            var result = new List<NameValuePair>(new[] {
                new NameValuePair{ Name = _localizor.Get("AllTime"), Value = "AllTime" },
                new NameValuePair{ Name = _localizor.Get("PreDefinedDateTimeRanges_Custom"), Value = "Custom" } });

            if (future)
            {
                result.AddRange(new[] {
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextTenYears"), Value = "NextTenYears" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextFiveYears"), Value = "NextFiveYears" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextYear"), Value = "NextYear" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextSixMonths"), Value = "NextSixMonths" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextThreeMonths"), Value = "NextThreeMonths" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextMonth"), Value = "NextMonth" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextWeek"), Value = "NextWeek" },
                new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Tomorrow"), Value = "Tomorrow" },
                });
            }

            result.AddRange(new[] {
                //new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisYear"), Value = "ThisYear" },
                //new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisMonth"), Value = "ThisMonth" },
                //new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisWeek"), Value = "ThisWeek" },
                new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Today"), Value = "Today" },
                });

            if (past)
            {
                result.AddRange(new[] {
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Yesterday"), Value = "Yesterday" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastWeek"), Value = "LastWeek" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastMonth"), Value = "LastMonth" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastThreeMonths"), Value = "LastThreeMonths" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastSixMonths"), Value = "LastSixMonths" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastYear"), Value = "LastYear" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastFiveYears"), Value = "LastFiveYears" },
                    new NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastTenYears"), Value = "LastTenYears" },
                });
            }

            return result;
        }

        public SelectList GetSelectList(List<NameValuePair>? nameValuePairs)
        {
            if (nameValuePairs == null)
                return new SelectList(Enumerable.Empty<SelectListItem>());
            return new SelectList(nameValuePairs, nameof(NameValuePair.Value), nameof(NameValuePair.Name));
        }
    }
}

