using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Common.Services;

public class AppLoadingService
{
    private const double Step10Progress = 0.1;
    private const double Step11Progress = 0.15;
    private const double Step20Progress = 0.2;
    private const double Step21Progress = 0.225;
    private const double Step22Progress = 0.25;
    private const double Step30Progress = 0.30;
    private const double Step31Progress = 0.35;

    private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService _authenticationService;
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.GeoLocationService _geoLocationService;
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService _appShellService;
    private readonly AdventureWorksLT2019.MauiXApp.Services.CustomerService _customerService;

    public AppLoadingService(
        Framework.MauiX.Services.SecureStorageService secureStorageService,
        AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService authenticationService,
        AdventureWorksLT2019.MauiXApp.Common.Services.GeoLocationService geoLocationService,
        AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService appShellService,
        AdventureWorksLT2019.MauiXApp.Services.CustomerService customerService
        )
    {
        _secureStorageService = secureStorageService;
        _authenticationService = authenticationService;
        _geoLocationService = geoLocationService;
        _appShellService = appShellService;

        _customerService = customerService;
    }

    public async Task Step1OnInitialAppLoading()
    {
        WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage(Step10Progress));

        // 1. TODO: add application level data here, if any more

        // 2. Auto Login
        //AppState = Framework.MauiX.Models.AppStates.Autheticating;
        var signInData = await _authenticationService.AutoLogInAsync();
        if (signInData.IsAuthenticated())
        {
            await Step2OnAuthenticated(false, signInData.GotoFirstTimeUserPage());
        }
        else
        {
            await AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.LogInPage));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reAssignAppLoadingPage">will be true when calling from LogInPage and RegisterUserPage</param>
    /// <param name="gotoFirstTimeUserPage"></param>
    /// <returns></returns>
    public async Task Step2OnAuthenticated(bool reAssignAppLoadingPage, bool gotoFirstTimeUserPage)
    {
        // 1. we know it is step #1 finished(Launching), we are going to load application level data 
        if (reAssignAppLoadingPage)
        {
            await AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.AppLoadingPage));
        }

        // 2.1. CurrentTheme
        WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage(Step20Progress));
        var currentAppTheme = Preferences.Default.Get<string>("CurrentAppTheme", AppTheme.Light.ToString());
        var currentAppThemeEnum = Enum.Parse<AppTheme>(currentAppTheme);
        Application.Current.UserAppTheme = currentAppThemeEnum;

        // 2.2. GetCurrentLocation
        WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage(Step21Progress));
        await _geoLocationService.GetCurrentLocation();

        // 3. Cache Application/User Level data if not first time user
        WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Common.Messages.AppLoadingProgressChangedMessage(Step31Progress));
        
        // SQLite Cache is disabled for now, because SQLite query not working properly.
        //await _customerService.CacheDeltaData();

        await _appShellService.AddFlyoutMenus(gotoFirstTimeUserPage);
    }
}
