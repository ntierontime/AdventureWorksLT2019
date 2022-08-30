using System.Windows.Input;

namespace Framework.MauiX.ViewModels
{
    public class ThemeSelectorVM : Framework.MauiX.PropertyChangedNotifier
    {
        public List<Framework.MauiX.DataModels.ThemeSelectorItem> Themes { get; private set; }

        public ICommand Command_ThemeSelected { get; set; }

        public ThemeSelectorVM()
        {
        }

        public void Initialize(ResourceDictionary lightTheme, ResourceDictionary darkTheme)
        {
            var currentTheme = Framework.Xaml.ApplicationPropertiesHelper.GetTheme();
            LightTheme = new ThemeSelectorItem
            {
                Theme = Framework.Themes.Theme.Light
                 ,
                IsCurrent = currentTheme == Framework.Themes.Theme.Light
                 ,
                Text = Framework.Resx.UIStringResource.Light
                ,
                ResourceDictionary = lightTheme
            };
            DarkTheme = new ThemeSelectorItem
            {
                Theme = Framework.Themes.Theme.Dark
                 ,
                IsCurrent = currentTheme == Framework.Themes.Theme.Dark
                 ,
                Text = Framework.Resx.UIStringResource.Dark
                ,
                ResourceDictionary = darkTheme
            };

            Command_ThemeSelected = new Command<Framework.Themes.Theme>(async t =>
            {
                LightTheme.IsCurrent = t == LightTheme.Theme;
                DarkTheme.IsCurrent = t == DarkTheme.Theme;

                await Framework.Xaml.ApplicationPropertiesHelper.SetTheme(t);
                SwitchTheme(t);
            });
        }

        public void SwitchTheme(Framework.Themes.Theme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case Framework.Themes.Theme.Dark:
                        mergedDictionaries.Add(DarkTheme.ResourceDictionary);
                        break;
                    case Framework.Themes.Theme.Light:
                    default:
                        mergedDictionaries.Add(LightTheme.ResourceDictionary);
                        break;
                }
            }
        }
    }
}

