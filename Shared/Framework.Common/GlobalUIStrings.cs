using Microsoft.Extensions.Localization;

namespace Framework.Common
{
    public class GlobalUIStrings : Framework.Common.IGlobalUIStrings
    {
        private readonly IStringLocalizer<Framework.Common.GlobalUIStrings> _localizer;
        public GlobalUIStrings(IStringLocalizer<Framework.Common.GlobalUIStrings> localizer)
        {
            _localizer = localizer;
        }

        public string Get(string key)
        {
            return _localizer[key];
        }
    }
}

