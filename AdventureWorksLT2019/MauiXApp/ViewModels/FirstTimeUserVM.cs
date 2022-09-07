using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class FirstTimeUserVM : ObservableObject
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.UserService _userService;

        public FirstTimeUserVM(
            AdventureWorksLT2019.MauiXApp.Services.UserService userService)
        {
            _userService = userService;
        }

        public ICommand SkipCommand => new Command(OnSkip, CanSkip);

        private async void OnSkip()
        {
            await AdventureWorksLT2019.MauiXApp.Services.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage));
        }
        private bool CanSkip()
        {
            return true;
        }

        public ICommand DoneCommand => new Command(OnDone, CanDone);

        private async void OnDone()
        {
            await _userService.SetUserProfileCompletedAsync();
            await AdventureWorksLT2019.MauiXApp.Services.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage));
        }
        private bool CanDone()
        {
            return true;
        }
    }
}
