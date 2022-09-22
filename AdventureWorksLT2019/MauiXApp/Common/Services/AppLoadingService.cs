using AdventureWorksLT2019.MauiXApp.Common.Messages;
using AdventureWorksLT2019.MauiXApp.Services;
using AdventureWorksLT2019.MauiXApp.Views;
using Framework.MauiX.Services;
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

    private readonly SecureStorageService _secureStorageService;
    private readonly AuthenticationService _authenticationService;
    private readonly GeoLocationService _geoLocationService;
    private readonly AppShellService _appShellService;
    private readonly AddressService _addressService;
    private readonly CustomerService _customerService;
    private readonly CustomerAddressService _customerAddressService;
    private readonly ProductService _productService;
    private readonly ProductCategoryService _productCategoryService;
    private readonly ProductDescriptionService _productDescriptionService;
    private readonly ProductModelService _productModelService;
    private readonly ProductModelProductDescriptionService _productModelProductDescriptionService;
    private readonly SalesOrderDetailService _salesOrderDetailService;
    private readonly SalesOrderHeaderService _salesOrderHeaderService;

    public AppLoadingService(
        SecureStorageService secureStorageService,
        AuthenticationService authenticationService,
        GeoLocationService geoLocationService,
        AppShellService appShellService,
        AddressService addressService,
        CustomerService customerService,
        CustomerAddressService customerAddressService,
        ProductService productService,
        ProductCategoryService productCategoryService,
        ProductDescriptionService productDescriptionService,
        ProductModelService productModelService,
        ProductModelProductDescriptionService productModelProductDescriptionService,
        SalesOrderDetailService salesOrderDetailService,
        SalesOrderHeaderService salesOrderHeaderService
        )
    {
        _secureStorageService = secureStorageService;
        _authenticationService = authenticationService;
        _geoLocationService = geoLocationService;
        _appShellService = appShellService;
        _addressService = addressService;
        _customerService = customerService;
        _customerAddressService = customerAddressService;
        _productService = productService;
        _productCategoryService = productCategoryService;
        _productDescriptionService = productDescriptionService;
        _productModelService = productModelService;
        _productModelProductDescriptionService = productModelProductDescriptionService;
        _salesOrderDetailService = salesOrderDetailService;
        _salesOrderHeaderService = salesOrderHeaderService;
    }

    public async Task Step1OnInitialAppLoading()
    {
        WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(Step10Progress));

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
            await AppShellService.GoToAbsoluteAsync(nameof(LogInPage));
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
            await AppShellService.GoToAbsoluteAsync(nameof(AppLoadingPage));
        }

        // 2.1. CurrentTheme
        WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(Step20Progress));
        var currentAppTheme = Preferences.Default.Get<string>("CurrentAppTheme", AppTheme.Light.ToString());
        var currentAppThemeEnum = Enum.Parse<AppTheme>(currentAppTheme);
        Application.Current.UserAppTheme = currentAppThemeEnum;

        // 2.2. GetCurrentLocation
        WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(Step21Progress));
        await _geoLocationService.GetCurrentLocation();

        // 3. Cache Application/User Level data if not first time user
        WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(Step31Progress));

        //// SQLite Cache is disabled for now, because SQLite query not working properly.
        //await _addressService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.4));
        //await _customerService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.45));
        //await _customerAddressService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.5));
        //await _productService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.55));
        //await _productCategoryService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.6));
        //await _productDescriptionService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.65));
        //await _productModelService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.7));
        //await _productModelProductDescriptionService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.75));
        //await _salesOrderDetailService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.8));
        //await _salesOrderHeaderService.CacheDeltaData();
        //WeakReferenceMessenger.Default.Send<AppLoadingProgressChangedMessage>(new AppLoadingProgressChangedMessage(0.85));

        await _appShellService.AddFlyoutMenus(gotoFirstTimeUserPage);
    }
}

