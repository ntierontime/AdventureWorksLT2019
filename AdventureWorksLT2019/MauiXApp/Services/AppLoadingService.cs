using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class AppLoadingService
    {
        private const double Step10Progress = 0.1;
        private const double Step11Progress = 0.15;
        private const double Step20Progress = 0.2;
        private const double Step21Progress = 0.25;

        private readonly Framework.MauiX.Services.IThemeService _themeService;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.AuthenticationService _authenticationService;
        private readonly AdventureWorksLT2019.MauiXApp.Services.GeoLocationService _geoLocationService;

        public AppLoadingService(
            Framework.MauiX.Services.IThemeService themeService,
            Framework.MauiX.Services.SecureStorageService secureStorageService,
            AdventureWorksLT2019.MauiXApp.Services.AuthenticationService authenticationService,
            AdventureWorksLT2019.MauiXApp.Services.GeoLocationService geoLocationService
            )
        {
            _themeService = themeService;
            _secureStorageService = secureStorageService;
            _authenticationService = authenticationService;
            _geoLocationService = geoLocationService;
        }

        public async Task Step1OnInitialAppLoading()
        {
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage(Step10Progress));

            // 1. Theme 
            var currentTheme = await _secureStorageService.GetCurrentTheme();
            _themeService.SwitchTheme(currentTheme);
            // _themeService.SwitchTheme(AppTheme.Dark);

            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage(Step11Progress));

            // 2. TODO: add application level data here, if any more


            // 3. Auto Login
            //AppState = Framework.MauiX.Models.AppStates.Autheticating;
            var signInData = await _authenticationService.AutoLogInAsync();
            if (signInData.IsAuthenticated())
            {
                await Step2OnAuthenticated(false, false);
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage));
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
                WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage(Step20Progress));
                await Shell.Current.GoToAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage));
            }

            //// 2. GetCurrentLocation
            //_progressBarVM.Forward();
            await _geoLocationService.GetCurrentLocation();
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage(Step21Progress));

            // 3. Get Other Application/User Level data if not first time user
            //AppState = Framework.MauiX.Models.AppStates.Running;
            if (!gotoFirstTimeUserPage)
            {
                // TODO: Get Other Application/User Level data if not first time user
                await Shell.Current.GoToAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage));
            }
            else
            {
                // TODO: Setup User Profile in FirstTimeUserPage
                await Shell.Current.GoToAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage));
            }
        }
    }
}
