using AdventureWorksLT2019.MauiXApp.ViewModels;

namespace AdventureWorksLT2019.MauiXApp;

public partial class App : Application
{
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.AppLoadingService _appLoadingService;
    public App()
    {
        InitializeComponent();
        //private AdventureWorksLT2019.MauiXApp.DataModels.AppStates _appStates;
        //_appStates = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.DataModels.AppStates>();
        _appLoadingService = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.Common.Services.AppLoadingService>();
        MainPage = new AdventureWorksLT2019.MauiXApp.AppShell();
    }

    protected override async void OnStart()
    {
        await _appLoadingService.Step1OnInitialAppLoading();
    }

    protected override void OnSleep()
    {
        // Handle when your app sleeps
    }

    protected override void OnResume()
    {
        // Handle when your app resumes
    }
}