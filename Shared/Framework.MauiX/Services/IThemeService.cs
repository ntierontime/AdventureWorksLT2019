using Framework.MauiX.DataModels;

namespace Framework.MauiX.Services
{
    public interface IThemeService
    {
        // List<ThemeSelectorItem> GetThemeSelectorItems();
        void SwitchTheme(AppTheme theme);
    }
}