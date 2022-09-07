using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class AppShellVM: ObservableObject
    {
        private FlyoutBehavior m_FlyoutBehavior = FlyoutBehavior.Disabled;
        public FlyoutBehavior FlyoutBehavior
        {
            get => m_FlyoutBehavior;
            set => SetProperty(ref m_FlyoutBehavior, value);
        }

        private readonly AdventureWorksLT2019.MauiXApp.Services.UserService _userService;

        public AppShellVM(
            AdventureWorksLT2019.MauiXApp.Services.UserService userService)
        {
            _userService = userService;
        }

        public ICommand LogOutCommand => new Command(OnLogOutAsync);
        private async void OnLogOutAsync()
        {
            // RemeberMe always "true"
            var succeeded = await _userService.LogOutAsync();
            if (succeeded)
            {
                await AdventureWorksLT2019.MauiXApp.Services.AppShellHelper.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage));
            }
        }
    }
}
