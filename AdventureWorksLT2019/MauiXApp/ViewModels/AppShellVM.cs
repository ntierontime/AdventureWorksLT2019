using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class AppShellVM: ObservableObject
    {
        private readonly AdventureWorksLT2019.MauiX.Services.AuthenticationService _authenticationService;

        public AppShellVM(
            MauiX.Services.AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ICommand LogOutCommand => new Command(OnLogOutAsync);
        private async void OnLogOutAsync()
        {
            // RemeberMe always "true"
            var succeeded = await _authenticationService.LogOutAsync();
            if (succeeded)
            {
                // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.LogInPage();
            }
        }
    }
}
