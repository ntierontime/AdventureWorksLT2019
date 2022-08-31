namespace AdventureWorksLT2019.MauiX.Themes
{
    public class ThemesHelper : Framework.MauiX.Helpers.IThemesHelper
    {
        public ResourceDictionary GetTheme(Framework.Common.Theme theme)
        {
            if (theme == Framework.Common.Theme.Dark)
                return new AdventureWorksLT2019.MauiX.Themes.DarkTheme();
            return new AdventureWorksLT2019.MauiX.Themes.LightTheme();
        }

        public List<Framework.MauiX.DataModels.ThemeSelectorItem> GetThemeSelectorItems()
        {
            var themeList = new List<Framework.MauiX.DataModels.ThemeSelectorItem>
            {
                new Framework.MauiX.DataModels.ThemeSelectorItem { Text = AdventureWorksLT2019.Resx.Resources.UIStrings.Light, Theme = Framework.Common.Theme.Light },
                new Framework.MauiX.DataModels.ThemeSelectorItem { Text = AdventureWorksLT2019.Resx.Resources.UIStrings.Dark, Theme = Framework.Common.Theme.Dark }
            };
            return themeList;
        }
        
        public void SwitchTheme(Framework.Common.Theme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(GetTheme(theme));
            }
        }
    }
}

