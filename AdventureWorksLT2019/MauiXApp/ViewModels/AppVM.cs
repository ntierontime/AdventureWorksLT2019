using CommunityToolkit.Mvvm.ComponentModel;
using Framework.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class AppVM : ObservableObject
    {
        public bool HasAuthentication { get; set; }

        //protected Framework.MauiX.Models.AppStates m_AppState = Framework.MauiX.Models.AppStates.Launching;
        //public Framework.MauiX.Models.AppStates AppState
        //{
        //    get => m_AppState;
        //    set => SetProperty(ref m_AppState, value);
        //}

        protected bool m_ShellNavBarIsVisible;
        public bool ShellNavBarIsVisible
        {
            get => m_ShellNavBarIsVisible;
            set => SetProperty(ref m_ShellNavBarIsVisible, value);
        }

        private Microsoft.Spatial.GeographyPoint m_CurrentLocation;
        public Microsoft.Spatial.GeographyPoint CurrentLocation
        {
            get => m_CurrentLocation;
            set => SetProperty(ref m_CurrentLocation, value);
        }

        private readonly AdventureWorksLT2019.MauiXApp.ViewModels.ProgressBarVM _progressBarVM;

        private readonly Framework.MauiX.Services.IThemeService _themeService;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
        private readonly AdventureWorksLT2019.MauiX.Services.AuthenticationService _authenticationService;

        public AppVM(
            Framework.MauiX.Services.IThemeService themeService, 
            Framework.MauiX.Services.SecureStorageService secureStorageService,
            AdventureWorksLT2019.MauiX.Services.AuthenticationService authenticationService,
            AdventureWorksLT2019.MauiXApp.ViewModels.ProgressBarVM progressBarVM
            )
        {
            _themeService = themeService;
            _secureStorageService = secureStorageService;
            _authenticationService = authenticationService;
            _progressBarVM = progressBarVM;
        }

        public async Task OnStart()
        {
            HasAuthentication = false;

            _progressBarVM.Initialization(1);
            _progressBarVM.Forward();

            // 1. Theme 
            var currentTheme = await _secureStorageService.GetCurrentTheme();
            _themeService.SwitchTheme(currentTheme);
            // _themeService.SwitchTheme(AppTheme.Dark);

            // 2. TODO: add application level data here, if any more

            // 3. Auto Login
            //AppState = Framework.MauiX.Models.AppStates.Autheticating;
            var signInData = await _authenticationService.AutoLogInAsync();
            if (signInData.IsAuthenticated())
            {
                await OnAuthenticated(false, false);
            }
            else
            {
                // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.LogInPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reAssignAppLoadingPage">will be true when calling from LogInPage and RegisterUserPage</param>
        /// <param name="gotoFirstTimeUserPage"></param>
        /// <returns></returns>
        public async Task OnAuthenticated(bool reAssignAppLoadingPage, bool gotoFirstTimeUserPage)
        {
            //AppState = Framework.MauiX.Models.AppStates.Loading;

            // 1. we know it is step #1 finished(Launching), we are going to load application level data 
            if (reAssignAppLoadingPage)
            {
                _progressBarVM.Go(0.1); 
                // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage();
            }

            // 2. GetCurrentLocation
            _progressBarVM.Forward();
            await GetCurrentLocation();

            // 3. Get Other Application/User Level data if not first time user
            //AppState = Framework.MauiX.Models.AppStates.Running;
            if (!gotoFirstTimeUserPage)
            {
                // TODO: Get Other Application/User Level data if not first time user
                Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.AppShell();
            }
            {
                // TODO: Setup User Profile in FirstTimeUserPage
                // Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage();
                // Assign MainPage to AppShell after FirstTimeUserPage Finished.
            }
        }

        private async Task GetCurrentLocation()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                        return;
                }
                Location location = null;
                for (var i = 0; i < 10; i++)
                {
                    if (CurrentLocation == null)
                    {
                        location = await Geolocation.Default.GetLastKnownLocationAsync();
                    }
                    if (CurrentLocation == null)
                    {
                        location = await Geolocation.Default.GetLocationAsync();
                    }

                    if (location == null)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        break;
                    }
                }
                if (location != null)
                {
                    CurrentLocation = Microsoft.Spatial.GeographyPoint.Create(location.Latitude, location.Longitude);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Handle not supported on device exception
            //}
            //catch (FeatureNotEnabledException fneEx)
            //{
            //    // Handle not enabled on device exception
            //}
            //catch (PermissionException pEx)
            //{
            //    // Handle permission exception
            //}
            catch //(Exception ex)
            {
                // Unable to get location
            }
        }
    }
}

