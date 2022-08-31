using Framework.Common;
using Framework.MauiX.DataModels;

namespace Framework.MauiX.Helpers
{
    public interface IThemesHelper
    {
        ResourceDictionary GetTheme(Theme theme);
        List<ThemeSelectorItem> GetThemeSelectorItems();
        void SwitchTheme(Framework.Common.Theme theme);
    }
}