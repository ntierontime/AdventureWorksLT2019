using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.WebApiClients;
using AdventureWorksLT2019.MauiXApp.ViewModels;
using AdventureWorksLT2019.MauiXApp.SQLite;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.SQLite;
using Framework.MauiX.Services;
using Framework.MauiX.Icons;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

namespace AdventureWorksLT2019.MauiXApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
#if ANDROID && DEBUG
            // https://stackoverflow.com/questions/71047509/trust-anchor-for-certification-path-not-found-in-a-net-maui-project-trying-t
            Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
            Platforms.Android.DangerousTrustProvider.Register();
#endif
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("MaterialIcons-Regular.ttf", MaterialIconFamilies.MaterialIconRegular);
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", MaterialIconFamilies.MaterialIconOutlined);
                    fonts.AddFont("MaterialIconsRound-Regular.otf", MaterialIconFamilies.MaterialIconRound);
                    fonts.AddFont("MaterialIconsSharp-Regular.otf", MaterialIconFamilies.MaterialIconSharp);
                    fonts.AddFont("MaterialIconsTwoTone-Regular.otf", MaterialIconFamilies.MaterialIconTwoTone);
                });

            builder.Services.AddLocalization();

            // 1. Essential
            builder.Services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
            builder.Services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);

            // 2.1. Framework.MauiX.Services
            builder.Services.AddSingleton<SecureStorageService>();
            builder.Services.AddSingleton<SQLiteService>();
            builder.Services.AddSingleton<CacheDataStatusRepository>();
            builder.Services.AddSingleton<CacheDataStatusService>();

            // 2.2. AdventureWorksLT2019.MauiX.WebApiClients
            builder.Services.AddScoped<WebApiConfig>();
            builder.Services.AddScoped<AuthenticationApiClient>();
            builder.Services.AddScoped<CodeListsApiClient>();

            // 2.3. AdventureWorksLT2019.MauiX.Services
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AppLoadingService>();
            builder.Services.AddScoped<GeoLocationService>();
            builder.Services.AddScoped<AppShellService>();
            builder.Services.AddScoped<CodeListsApiService>();

            // 2.4. DataModels
            builder.Services.AddSingleton<AppStates>();

            // 2.5. ViewModels
            // 2.5.1. Global Page ViewModels
            // 2.5.1.1. AppVM is a Singleton/Global, most of others are Scoped(new instance)
            builder.Services.AddSingleton<AppVM>();
            builder.Services.AddSingleton<AppLoadingVM>();
            builder.Services.AddScoped<LogInVM>();
            builder.Services.AddScoped<RegisterUserVM>();
            builder.Services.AddScoped<FirstTimeUserVM>();
            builder.Services.AddScoped<SettingsVM>();
            builder.Services.AddScoped<WebAuthenticatorViewModel>();

            // 3.1. WebApi Clients
            builder.Services.AddScoped<BuildVersionApiClient>();
            builder.Services.AddScoped<ErrorLogApiClient>();
            builder.Services.AddScoped<AddressApiClient>();
            builder.Services.AddScoped<CustomerApiClient>();
            builder.Services.AddScoped<CustomerAddressApiClient>();
            builder.Services.AddScoped<ProductApiClient>();
            builder.Services.AddScoped<ProductCategoryApiClient>();
            builder.Services.AddScoped<ProductDescriptionApiClient>();
            builder.Services.AddScoped<ProductModelApiClient>();
            builder.Services.AddScoped<ProductModelProductDescriptionApiClient>();
            builder.Services.AddScoped<SalesOrderDetailApiClient>();
            builder.Services.AddScoped<SalesOrderHeaderApiClient>();

            // 3.2. SQLite Repositories - Register if wannt to cache.

            // 3.3. Services
            builder.Services.AddScoped<BuildVersionService>();
            builder.Services.AddScoped<ErrorLogService>();
            builder.Services.AddScoped<AddressService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<CustomerAddressService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductCategoryService>();
            builder.Services.AddScoped<ProductDescriptionService>();
            builder.Services.AddScoped<ProductModelService>();
            builder.Services.AddScoped<ProductModelProductDescriptionService>();
            builder.Services.AddScoped<SalesOrderDetailService>();
            builder.Services.AddScoped<SalesOrderHeaderService>();

            // 3.4.1. View Models. ListVM
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.BuildVersion.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ErrorLog.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Address.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Product.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader.ListVM>();

            // 3.4.2. View Models. ItemVM
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.BuildVersion.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ErrorLog.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Address.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Product.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail.ItemVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader.ItemVM>();

            // 3.4.3. View Models. DashboardVM
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Address.DashboardVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.DashboardVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Product.DashboardVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory.DashboardVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription.DashboardVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel.DashboardVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader.DashboardVM>();

            return builder.Build();
        }
    }
}

