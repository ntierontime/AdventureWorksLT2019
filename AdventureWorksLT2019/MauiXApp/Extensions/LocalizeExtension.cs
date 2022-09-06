using Microsoft.Extensions.Localization;

namespace AdventureWorksLT2019.MauiXApp.Extensions
{
    [ContentProperty(nameof(Key))]
    public class LocalizeExtension : IMarkupExtension
    {
        IStringLocalizer<AdventureWorksLT2019.Resx.Resources.UIStrings> _localizer;

        public string Key { get; set; } = string.Empty;

        public LocalizeExtension()
        {
            _localizer = Framework.MauiX.Helpers.ServiceHelper.GetService<IStringLocalizer<AdventureWorksLT2019.Resx.Resources.UIStrings>>();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            
            string localizedText = _localizer[Key];
            return localizedText;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}
