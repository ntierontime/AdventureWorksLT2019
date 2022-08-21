using Microsoft.Extensions.Localization;

namespace AdventureWorksLT2019.Resx
{
    public class UIStrings : IUIStrings
    {
        private readonly IStringLocalizer<UIStrings> _localizer;
        public UIStrings(IStringLocalizer<UIStrings> localizer)
        {
            _localizer = localizer;
        }

        public string Get(string key)
        {
            return _localizer[key];
        }
    }
}

