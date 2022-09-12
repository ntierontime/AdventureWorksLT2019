using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Services.Common
{
    public class AppLoadingService
    {
        private const double Step10Progress = 0.1;
        private const double Step11Progress = 0.15;
        private const double Step20Progress = 0.2;
        private const double Step21Progress = 0.225;
        private const double Step22Progress = 0.25;
        private const double Step30Progress = 0.30;
        private const double Step31Progress = 0.35;

        private readonly Framework.MauiX.Services.IThemeService _themeService;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.AuthenticationService _authenticationService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.GeoLocationService _geoLocationService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService _appShellService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.CustomerService _customerService;

        public AppLoadingService(
            Framework.MauiX.Services.IThemeService themeService,
            Framework.MauiX.Services.SecureStorageService secureStorageService,
            AdventureWorksLT2019.MauiXApp.Services.Common.AuthenticationService authenticationService,
            AdventureWorksLT2019.MauiXApp.Services.Common.GeoLocationService geoLocationService,
            AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService appShellService,
            AdventureWorksLT2019.MauiXApp.Services.CustomerService customerService
            )
        {
            _themeService = themeService;
            _secureStorageService = secureStorageService;
            _authenticationService = authenticationService;
            _geoLocationService = geoLocationService;
            _appShellService = appShellService;

            _customerService = customerService;
        }

        public async Task Step1OnInitialAppLoading()
        {
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step10Progress));

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
                await AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.Common.LogInPage));
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
                await AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.Common.AppLoadingPage));
            }

            // 2.1. CurrentTheme
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step20Progress));
            var currentAppTheme = Preferences.Default.Get<string>("CurrentAppTheme", AppTheme.Light.ToString());
            var currentAppThemeEnum = Enum.Parse<AppTheme>(currentAppTheme);
            Application.Current.UserAppTheme = currentAppThemeEnum;

            // 2.2. GetCurrentLocation
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step21Progress));
            await _geoLocationService.GetCurrentLocation();

            // 3. Cache Application/User Level data if not first time user
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step31Progress));
            await _customerService.CacheDeltaData();

            await _appShellService.AddFlyoutMenusDetails(gotoFirstTimeUserPage);
        }
    }
}
