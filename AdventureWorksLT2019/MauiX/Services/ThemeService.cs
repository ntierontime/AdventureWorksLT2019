namespace AdventureWorksLT2019.MauiX.Services
{
    public class ThemeService : Framework.MauiX.Helpers.IThemeService
    {
        public List<Framework.MauiX.DataModels.ThemeSelectorItem> GetThemeSelectorItems()
        {
            var themeList = new List<Framework.MauiX.DataModels.ThemeSelectorItem>
            {
                new Framework.MauiX.DataModels.ThemeSelectorItem { Text = AdventureWorksLT2019.Resx.Resources.UIStrings.Light, Theme = AppTheme.Light },
                new Framework.MauiX.DataModels.ThemeSelectorItem { Text = AdventureWorksLT2019.Resx.Resources.UIStrings.Dark, Theme = AppTheme.Dark }
            };
            return themeList;
        }
        
        public void SwitchTheme(AppTheme theme)
        {
            Application.Current.UserAppTheme = theme;
        }
    }
}

