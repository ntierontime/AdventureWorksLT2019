using AdventureWorksLT2019.MauiXApp.Common.Services;
using Framework.MauiX.Helpers;

namespace AdventureWorksLT2019.MauiXApp;

public partial class App : Application
{
    private readonly AppLoadingService _appLoadingService;
    public App()
    {
        _appLoadingService = ServiceHelper.GetService<AppLoadingService>();
        InitializeComponent();
        MainPage = new AppShell();
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

