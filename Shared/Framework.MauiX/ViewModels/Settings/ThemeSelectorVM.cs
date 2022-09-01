using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Framework.MauiX.ViewModels.Settings
{
    public class ThemeSelectorVM : Framework.MauiX.PropertyChangedNotifier
    {
        public List<Framework.MauiX.DataModels.ThemeSelectorItem> Themes { get; private set; }

        protected AppTheme _CurrentTheme;
        public AppTheme CurrentTheme
        {
            get
            {
                return _CurrentTheme;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(CurrentTheme), ref _CurrentTheme, value);
            }
        }

        public ICommand Command_ThemeSelected { get; set; }

        private readonly Framework.MauiX.Helpers.IThemeService _themesHelper;

        public ThemeSelectorVM(Framework.MauiX.Helpers.IThemeService themesHelper)
        {
            _themesHelper = themesHelper;
        }

        public async Task Initialize()
        {
            Themes = _themesHelper.GetThemeSelectorItems();
            CurrentTheme = await Framework.MauiX.Helpers.ApplicationPropertiesHelper.GetCurrentTheme();
            Command_ThemeSelected = new Command<AppTheme>(async t =>
            {
                await Framework.MauiX.Helpers.ApplicationPropertiesHelper.SetTheme(t);
                CurrentTheme = t;
                _themesHelper.SwitchTheme(t);
            });
        }
    }
}
