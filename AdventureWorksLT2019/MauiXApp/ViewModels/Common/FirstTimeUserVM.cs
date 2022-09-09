using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Common
{
    public class FirstTimeUserVM : ObservableObject
    {
        private readonly Services.Common.UserService _userService;

        public FirstTimeUserVM(
            Services.Common.UserService userService)
        {
            _userService = userService;
        }

        public ICommand SkipCommand => new Command(OnSkip, CanSkip);

        private async void OnSkip()
        {
            await Services.Common.AppShellService.GoToAbsoluteAsync(nameof(Pages.Common.MainPage));
        }
        private bool CanSkip()
        {
            return true;
        }

        public ICommand DoneCommand => new Command(OnDone, CanDone);

        private async void OnDone()
        {
            await _userService.SetUserProfileCompletedAsync();
            await Services.Common.AppShellService.GoToAbsoluteAsync(nameof(Pages.Common.MainPage));
        }
        private bool CanDone()
        {
            return true;
        }
    }
}
