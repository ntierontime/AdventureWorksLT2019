using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class SettingsVM : ObservableObject
    {
        public List<Framework.MauiX.DataModels.AppThemeItem> AppThemeItems { get; private set; }
        public List<Framework.MauiX.DataModels.LanguageItem> LanguageItems { get; private set; }

        private Framework.MauiX.DataModels.AppThemeItem m_CurrentAppTheme;
        public Framework.MauiX.DataModels.AppThemeItem CurrentAppTheme
        {
            get => m_CurrentAppTheme;
            set
            {
                SetProperty(ref m_CurrentAppTheme, value);
                Preferences.Default.Set(nameof(CurrentAppTheme), value.AppTheme.ToString());
                Application.Current.UserAppTheme = value.AppTheme;
            }
        }

        private Framework.MauiX.DataModels.LanguageItem m_CurrentLanguage;
        public Framework.MauiX.DataModels.LanguageItem CurrentLanguage
        {
            get => m_CurrentLanguage;
            set
            {
                SetProperty(ref m_CurrentLanguage, value);
                Preferences.Default.Set(nameof(CurrentLanguage), value.Language);
                CultureInfo.CurrentUICulture = new CultureInfo(value.Language, false);
            }
        }

        public SettingsVM()
        {
            AppThemeItems = new List<Framework.MauiX.DataModels.AppThemeItem>
            {
                new Framework.MauiX.DataModels.AppThemeItem { AppTheme = AppTheme.Light, AppThemeName = Resx.Resources.UIStrings.Light },
                new Framework.MauiX.DataModels.AppThemeItem { AppTheme = AppTheme.Dark, AppThemeName = Resx.Resources.UIStrings.Dark }
            };
            LanguageItems = new List<Framework.MauiX.DataModels.LanguageItem>
            {
                new Framework.MauiX.DataModels.LanguageItem { Language = "en", LanguageName = Resx.Resources.UIStrings.English },
                new Framework.MauiX.DataModels.LanguageItem { Language = "fr", LanguageName = Resx.Resources.UIStrings.French },
                new Framework.MauiX.DataModels.LanguageItem { Language = "zh-Hans", LanguageName = Resx.Resources.UIStrings.ChinesseSimplified },
                new Framework.MauiX.DataModels.LanguageItem { Language = "es", LanguageName = Resx.Resources.UIStrings.Spanish },
                new Framework.MauiX.DataModels.LanguageItem { Language = "ar", LanguageName = Resx.Resources.UIStrings.Arabic },
            };

            {
                if (Preferences.Default.ContainsKey(nameof(CurrentAppTheme)))
                {
                    var inPreferences = Preferences.Default.Get(nameof(CurrentAppTheme), AppTheme.Light.ToString());
                    m_CurrentAppTheme = AppThemeItems.First(t => t.AppTheme.ToString() == inPreferences);
                }
                else
                {
                    m_CurrentAppTheme = AppThemeItems.First();
                }
            }
            {
                if (Preferences.Default.ContainsKey(nameof(CurrentLanguage)))
                {
                    var inPreferences = Preferences.Default.Get(nameof(CurrentLanguage), "en");
                    m_CurrentLanguage = LanguageItems.First(t => t.Language == inPreferences);
                }
                else
                {
                    m_CurrentLanguage = LanguageItems.First();
                }
            }
        }
    }
}
