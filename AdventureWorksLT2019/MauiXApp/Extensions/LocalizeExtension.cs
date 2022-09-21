using AdventureWorksLT2019.Resx.Resources;
using Framework.MauiX.Helpers;
using Microsoft.Extensions.Localization;

namespace AdventureWorksLT2019.MauiXApp.Extensions;

[ContentProperty(nameof(Key))]
public class LocalizeExtension : IMarkupExtension
{
    IStringLocalizer<UIStrings> _localizer;

    public string Key { get; set; } = string.Empty;

    public LocalizeExtension()
    {
        _localizer = ServiceHelper.GetService<IStringLocalizer<UIStrings>>();
    }

    public object ProvideValue(IServiceProvider serviceProvider)
    {

        string localizedText = _localizer[Key];
        return localizedText;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
}

