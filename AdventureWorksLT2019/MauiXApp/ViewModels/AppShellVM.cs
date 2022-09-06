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

        private readonly AdventureWorksLT2019.MauiXApp.Services.AuthenticationService _authenticationService;

        public AppShellVM(
            AdventureWorksLT2019.MauiXApp.Services.AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //public ICommand LogOutCommand => new Command(OnLogOutAsync);
        //private async void OnLogOutAsync()
        //{
        //    // RemeberMe always "true"
        //    var succeeded = await _authenticationService.LogOutAsync();
        //    if (succeeded)
        //    {
        //        // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.LogInPage();
        //    }
        //}
    }
}
