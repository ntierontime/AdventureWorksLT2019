using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiX.ViewModels.Settings
{
    public class ThemeSelectorVM : ObservableObject
    {
        public List<Framework.MauiX.DataModels.ThemeSelectorItem> Themes { get; private set; }

        protected AppTheme m_CurrentTheme;
        public AppTheme CurrentTheme
        {
            get => m_CurrentTheme;
            set => SetProperty(ref m_CurrentTheme, value);
        }

        public ICommand Command_ThemeSelected { get; set; }

        private readonly Framework.MauiX.Services.IThemeService _themeService;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public AdventureWorksLT2019.MauiX.ViewModels.ProgressBarVM ProgressBarVM
        {
            get { return DependencyService.Resolve<AdventureWorksLT2019.MauiX.ViewModels.ProgressBarVM>(DependencyFetchTarget.GlobalInstance); }
        }

        public ThemeSelectorVM(
            Framework.MauiX.Services.IThemeService themeService,
            Framework.MauiX.Services.SecureStorageService secureStorageService
            )
        {
            _themeService = themeService;
            _secureStorageService = secureStorageService;
        }

        public async Task Initialize()
        {
            Themes = _themeService.GetThemeSelectorItems();
            CurrentTheme = await _secureStorageService.GetCurrentTheme();
            Command_ThemeSelected = new Command<AppTheme>(async t =>
            {
                await _secureStorageService.SetTheme(t);
                CurrentTheme = t;
                _themeService.SwitchTheme(t);
            });
        }
    }
}
