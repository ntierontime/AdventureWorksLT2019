namespace AdventureWorksLT2019.MauiX.ViewModels
{
    public class AppVM : Framework.MauiX.ViewModels.AppVMBase
    {
        private readonly Framework.MauiX.Services.IThemeService _themeService;
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;

        public Framework.MauiX.ViewModels.ProgressBarVM ProgressBarVM
        {
            get { return DependencyService.Resolve<Framework.MauiX.ViewModels.ProgressBarVM>(DependencyFetchTarget.GlobalInstance); }
        }

        public AppVM(
            Framework.MauiX.Services.IThemeService themeService, 
            Framework.MauiX.Services.SecureStorageService secureStorageService
            )
        {
            _themeService = themeService;
            _secureStorageService = secureStorageService;
        }

        public async Task OnStart()
        {
            HasAuthentication = false;

            // 2. 
            var currentTheme = await _secureStorageService.GetCurrentTheme();
            _themeService.SwitchTheme(currentTheme);

            // 3. 
            var signInData = await _secureStorageService.GetSignInData();
            // 1. Goto LogInPage
            if (!signInData.AutoSignIn)
            {

                Application.Current.MainPage = new AdventureWorksLT2019.MauiX.Pages.Account.LogInPage();
            }
            else
            {
                Application.Current.MainPage = new AdventureWorksLT2019.MauiX.AppShell();
            }

            ProgressBarVM.Initialization(4);
            ProgressBarVM.Forward();
            Thread.Sleep(2000);
            ProgressBarVM.Forward();
            Thread.Sleep(2000);
            ProgressBarVM.Forward();
            Thread.Sleep(2000);
            ProgressBarVM.Forward();
        }


        //public Elmah.XamarinForms.ViewModels.AppLoadingVM AppLoadingVM
        //{
        //    get { return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppLoadingVM>(DependencyFetchTarget.GlobalInstance); }
        //}

        //public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>();
        //    }
        //}

        //protected Elmah.XamarinForms.ViewModels.DashboardVM DashboardVM
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.DashboardVM>();
        //    }
        //}

        //public Elmah.MVVMLightViewModels.SQLiteFactory Caching
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<Elmah.MVVMLightViewModels.SQLiteFactory>();
        //    }
        //}

        //private List<Framework.Xaml.DomainModel> m_DomainRegistrationModels = new List<Framework.Xaml.DomainModel>();
        //public List<Framework.Xaml.DomainModel> DomainRegistrationModels
        //{
        //    get { return m_DomainRegistrationModels; }
        //    protected set
        //    {
        //        Set(nameof(DomainRegistrationModels), ref m_DomainRegistrationModels, value);
        //    }
        //}

        //private List<Framework.Xamariner.Interfaces.IDomainManager> m_DomainManagers = new List<Framework.Xamariner.Interfaces.IDomainManager>();
        //public List<Framework.Xamariner.Interfaces.IDomainManager> DomainManagers
        //{
        //    get { return m_DomainManagers; }
        //    protected set
        //    {
        //        Set(nameof(DomainManagers), ref m_DomainManagers, value);
        //    }
        //}

        // public void Initialize(bool hasAuthentication = true)
        // {
        // HasAuthentication = hasAuthentication;

        //            // 1. initialize

        //            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Framework.Resx.UIStringResource));
        //            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Elmah.Resx.UIStringResourcePerApp));
        //            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Elmah.Resx.UIStringResourcePerEntity));
        //            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Elmah.Resx.UIStringResourceReport));

        //            Framework.Models.PropertyChangedNotifierHelper.Initialize(true);
        //            Framework.Weather.WeatherServiceSingleton.Instance.InitWeatherServiceProvider(new Framework.Weather.OpenWeatherMap.OpenWeatherMapProvider());
        //            Elmah.MVVMLightViewModels.SQLiteFactory.Initialize();
        //            Framework.Helpers.GeoHelperSinglton.Instance.Init("3");

        //            // 2. Web Service Url and Toke
        //            if (Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.Android.ToString().ToLower())
        //            {
        //                Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl = Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl_Android;
        //            }
        //            else if (Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.iOS.ToString().ToLower())
        //            {
        //                Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl = Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl_IOS;
        //            }
        //            else
        //            {
        //                Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl = Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl_General;
        //            }

        //#if DEBUG
        //            // TODO: change to false if want to test/enable/disable XXX.MVVMLightViewModels.WebServiceConfig.UseToken
        //            Elmah.MVVMLightViewModels.WebServiceConfig.UseToken = true;
        //#else
        //            Elmah.MVVMLightViewModels.WebServiceConfig.UseToken = true;
        //#endif
        //            // 3. Initialize Localization
        //            if (Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.iOS.ToString().ToLower() || Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.Android.ToString().ToLower())
        //            {
        //                // determine the correct, supported .NET culture
        //                var ci = DependencyService.Get<Framework.Xamariner.ILocalize>().GetCurrentCultureInfo();
        //                Framework.Resx.UIStringResource.Culture = ci; // set the RESX for resource localization
        //                Elmah.Resx.UIStringResourcePerApp.Culture = ci; // set the RESX for resource localization

        //                Elmah.Resx.UIStringResourcePerEntity.Culture = ci; // set the RESX for resource localization
        //                Elmah.Resx.UIStringResourceReport.Culture = ci; // set the RESX for resource localization

        //                DependencyService.Get<Framework.Xamariner.ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
        //            }

        //            // 5. Register ViewModels
        //            Elmah.MVVMLightViewModels.ViewModelLocator._RegisterViewModels();
        //            Elmah.XamarinForms.ViewModels.ViewModelLocator.RegisterViewModels();

        //            // 6. Register Domains
        //            RegisterDomains();
        //            foreach (var domainManager in DomainManagers)
        //            {
        //                // 6.1. Register domain specific ViewModels
        //                domainManager.RegisterViewModels();
        //                // 6.2. Get DomainRegistrationModel, DomainRegistrationModel.Routers will be called in NavigationVM.RegisterRoutes()
        //                this.DomainRegistrationModels.Add(domainManager.CreateDomainModel());
        //            }

        //            // 7.
        //            AppShellVM.Cleanup();
        // }

        //        private void RegisterDomains()
        //        {
        //            // Add instances of all Framework.Xamariner.Interfaces.IDomainManager types to register domains and routes.
        //            var typeOfIDomainManager = typeof(Framework.Xamariner.Interfaces.IDomainManager);
        //            var concreteTypesOfIDomainManager = AppDomain.CurrentDomain.GetAssemblies()
        //                .SelectMany(s => s.GetTypes())
        //                .Where(p => typeOfIDomainManager.IsAssignableFrom(p) && (p.IsClass && !p.IsAbstract));

        //            foreach(var concreteTypeOfIDomainManager in concreteTypesOfIDomainManager)
        //            {
        //                var instance = (Framework.Xamariner.Interfaces.IDomainManager)Activator.CreateInstance(concreteTypeOfIDomainManager);
        //                this.DomainManagers.Add(instance);
        //            }
        //        }

        //    public async Task OnStart()
        //{
        //    HasAuthentication = hasAuthentication;

        //    var currentTheme = await Framework.MauiX.Helpers.ApplicationPropertiesHelper.GetCurrentTheme();
        //    _themesHelper.SwitchTheme(currentTheme);

        //if (!HasAuthentication)
        //{
        //    MessagingCenter.Send<Elmah.XamarinForms.ViewModels.DashboardVM, long>(DashboardVM, Elmah.XamarinForms.ViewModels.DashboardVM.MessageTitle_LoadData, -1);
        //    return;
        //}

        //try
        //{
        //    App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.AppLoadingPage());

        //    // Load SignInData
        //    SignInData = Framework.Xaml.ApplicationPropertiesHelper.GetSignInData();

        //    if (SignInData.RememberMe && SignInData.AutoSignIn)
        //    {
        //        var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateAuthenticationApiClient();

        //        var LoginResponse = await client.LogInAsync(SignInData.UserName, SignInData.Password);
        //        if (LoginResponse.Succeeded)
        //        {
        //            LoginResponse.LoggedInSource = Framework.WebApi.LoggedInSource.AutoLogIn;

        //            await Framework.Xaml.ApplicationPropertiesHelper.SetSignInData(
        //                SignInData.UserName
        //                , SignInData.Password
        //                , SignInData.RememberMe, SignInData.AutoSignIn, LoginResponse.Token, LoginResponse.EntityID ?? 0, !LoginResponse.EntityID.HasValue, null);

        //            SignInData = Framework.Xaml.ApplicationPropertiesHelper.GetSignInData();

        //            App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.AppLoadingPage());
        //            MessagingCenter.Send<Elmah.XamarinForms.ViewModels.AppLoadingVM, Framework.WebApi.AuthenticationResponse>(AppLoadingVM, Elmah.XamarinForms.ViewModels.AppLoadingVM.MessageTitle_LoadData, LoginResponse);
        //        }
        //        else
        //        {
        //            await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
        //            App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.LogInPage());
        //        }
        //    }
        //    else
        //    {
        //        await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
        //        App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.LogInPage());
        //    }
        //    // TODO: if you want to track current GPS location
        //    //Framework.Xamariner.CrossGeolocatorHelper.locationChangedHandler += OnLocationChanged;
        //    //CurrentLocation = await Framework.Xamariner.CrossGeolocatorHelper.GetCurrentLocationAsync(null);
        //}
        //catch (System.Exception ex)
        //{
        //    await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
        //    App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.LogInPage());
        //}
        // }
    }
}

