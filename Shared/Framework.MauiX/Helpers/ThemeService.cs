using Framework.Common;
using Framework.MauiX.DataModels;

namespace Framework.MauiX.Helpers
{
    public interface IThemeService
    {
        List<ThemeSelectorItem> GetThemeSelectorItems();
        void SwitchTheme(AppTheme theme);
    }
}