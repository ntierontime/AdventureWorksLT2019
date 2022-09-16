using AdventureWorksLT2019.Resx;
using Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class SelectListHelper
    {
        private readonly AdventureWorksLT2019.Resx.IUIStrings _localizor;

        public SelectListHelper(AdventureWorksLT2019.Resx.IUIStrings localizor)
        {
            _localizor = localizor;
        }

        public List<Framework.Models.NameValuePair> GetDefaultTrueFalseBooleanSelectList()
        {
            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair { Name = _localizor.Get("All"), Value = "", Selected=true },
                new Framework.Models.NameValuePair { Name = _localizor.Get("True"), Value = "True"  },
                new Framework.Models.NameValuePair { Name = _localizor.Get("False"), Value = "False"  },
            });
        }

        public List<Framework.Models.NameValuePair> GetTextSearchTypeList()
        {
            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair { Name = _localizor.Get(TextSearchTypes.Contains.ToString()), Value = "", Selected=true },
                new Framework.Models.NameValuePair { Name = _localizor.Get(TextSearchTypes.StartsWith.ToString()), Value = TextSearchTypes.StartsWith.ToString() },
                new Framework.Models.NameValuePair { Name = _localizor.Get(TextSearchTypes.EndsWith.ToString()), Value = TextSearchTypes.EndsWith.ToString()},
            });
        }

        public List<Framework.Models.NameValuePair> GetDefaultPageSizeList()
        {
            var format_ItemsPerPage = _localizor.Get("Format_ItemsPerPage");

            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair { Name = string.Format(format_ItemsPerPage, 10), Value = "10", Selected=true },
                new Framework.Models.NameValuePair { Name = string.Format(format_ItemsPerPage, 25), Value = "25" },
                new Framework.Models.NameValuePair { Name = string.Format(format_ItemsPerPage, 50), Value = "50" },
                new Framework.Models.NameValuePair { Name = string.Format(format_ItemsPerPage, 100), Value = "100" },
            });
        }

        public List<Framework.Models.NameValuePair> GetDefaultPredefinedDateTimeRange(bool past = true, bool future = false)
        {
            var result = new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair { Name = _localizor.Get("AllTime"), Value = Framework.Models.PreDefinedDateTimeRanges.AllTime.ToString() },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Custom"), Value = Framework.Models.PreDefinedDateTimeRanges.Custom.ToString() } });

            if (future)
            {
                result.AddRange(new[] {
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextTenYears"), Value = Framework.Models.PreDefinedDateTimeRanges.NextTenYears.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextFiveYears"), Value = Framework.Models.PreDefinedDateTimeRanges.NextFiveYears.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextYear"), Value = Framework.Models.PreDefinedDateTimeRanges.NextYear.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextSixMonths"), Value = Framework.Models.PreDefinedDateTimeRanges.NextSixMonths.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextThreeMonths"), Value = Framework.Models.PreDefinedDateTimeRanges.NextThreeMonths.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextMonth"), Value = Framework.Models.PreDefinedDateTimeRanges.NextMonth.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextWeek"), Value = Framework.Models.PreDefinedDateTimeRanges.NextWeek.ToString() },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Tomorrow"), Value = Framework.Models.PreDefinedDateTimeRanges.Tomorrow.ToString() },
                });
            }

            result.AddRange(new[] {
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisYear"), Value = Framework.Models.PreDefinedDateTimeRanges.ThisYear.ToString() },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisMonth"), Value = Framework.Models.PreDefinedDateTimeRanges.ThisMonth.ToString() },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisWeek"), Value = Framework.Models.PreDefinedDateTimeRanges.ThisWeek.ToString() },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Today"), Value = Framework.Models.PreDefinedDateTimeRanges.Today.ToString() },
                });

            if (past)
            {
                result.AddRange(new[] {
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Yesterday"), Value = Framework.Models.PreDefinedDateTimeRanges.Yesterday.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastWeek"), Value = Framework.Models.PreDefinedDateTimeRanges.LastWeek.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastMonth"), Value = Framework.Models.PreDefinedDateTimeRanges.LastMonth.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastThreeMonths"), Value = Framework.Models.PreDefinedDateTimeRanges.LastThreeMonths.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastSixMonths"), Value = Framework.Models.PreDefinedDateTimeRanges.LastSixMonths.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastYear"), Value = Framework.Models.PreDefinedDateTimeRanges.LastYear.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastFiveYears"), Value = Framework.Models.PreDefinedDateTimeRanges.LastFiveYears.ToString() },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastTenYears"), Value = Framework.Models.PreDefinedDateTimeRanges.LastTenYears.ToString() },
                });
            }

            return result;
        }

        public SelectList GetSelectList(List<Framework.Models.NameValuePair>? nameValuePairs)
        {
            if (nameValuePairs == null)
                return new SelectList(Enumerable.Empty<SelectListItem>());
            return new SelectList(nameValuePairs, nameof(Framework.Models.NameValuePair.Value), nameof(Framework.Models.NameValuePair.Name));
        }
    }
}

