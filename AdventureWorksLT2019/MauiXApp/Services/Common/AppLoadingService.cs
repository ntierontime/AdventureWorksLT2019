using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Services.Common
{
    public class AppLoadingService
    {
        private const double Step10Progress = 0.1;
        private const double Step11Progress = 0.15;
        private const double Step20Progress = 0.2;
        private const double Step21Progress = 0.25;

        private readonly Framework.MauiX.Services.IThemeService _themeService;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.AuthenticationService _authenticationService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.GeoLocationService _geoLocationService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService _appShellService;

        public AppLoadingService(
            Framework.MauiX.Services.IThemeService themeService,
            Framework.MauiX.Services.SecureStorageService secureStorageService,
            AdventureWorksLT2019.MauiXApp.Services.Common.AuthenticationService authenticationService,
            AdventureWorksLT2019.MauiXApp.Services.Common.GeoLocationService geoLocationService,
            AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService appShellService
            )
        {
            _themeService = themeService;
            _secureStorageService = secureStorageService;
            _authenticationService = authenticationService;
            _geoLocationService = geoLocationService;
            _appShellService = appShellService;
        }

        public async Task Step1OnInitialAppLoading()
        {
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step10Progress));
            // 1. Theme 
            var currentTheme = await _secureStorageService.GetCurrentTheme();
            _themeService.SwitchTheme(currentTheme);
            // _themeService.SwitchTheme(AppTheme.Dark);

            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step11Progress));

            // 2. TODO: add application level data here, if any more

            // 3. Auto Login
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
                WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step20Progress));
            }

            //// 2. GetCurrentLocation
            //_progressBarVM.Forward();
            await _geoLocationService.GetCurrentLocation();
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.Common.AppLoadingProgressChangedMessage(Step21Progress));

            // 3. Get Other Application/User Level data if not first time user

            await _appShellService.AddFlyoutMenusDetails(gotoFirstTimeUserPage);
        }
    }
}
