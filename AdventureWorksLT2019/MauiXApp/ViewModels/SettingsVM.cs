using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class SettingsVM: ObservableObject
    {
        private AppTheme m_CurrentAppTheme;
        public AppTheme CurrentAppTheme
        {
            get => m_CurrentAppTheme;
            set => SetProperty(ref m_CurrentAppTheme, value);
        }

        private string m_CurrentLanguage;
        public string CurrentLanguage
        {
            get => m_CurrentLanguage;
            set => SetProperty(ref m_CurrentLanguage, value);
        }

        public ICommand ChangeAppThemeCommand { private set; get; }
        public ICommand ChangeLanguageCommand { private set; get; }

        public SettingsVM()
        {
            ChangeAppThemeCommand = new Command<AppTheme>(
                execute: (AppTheme arg) =>
                {
                    CurrentAppTheme = arg;
                });

            ChangeLanguageCommand = new Command<string>(
                execute: (string arg) =>
                {
                    CurrentLanguage = arg;
                });
        }
    }
}
