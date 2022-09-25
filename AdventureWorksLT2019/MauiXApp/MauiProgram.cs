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

            // 2.3. AdventureWorksLT2019.MauiX.Services
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AppLoadingService>();
            builder.Services.AddScoped<GeoLocationService>();
            builder.Services.AddScoped<AppShellService>();

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
            builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<CustomerRepository>();
            builder.Services.AddScoped<CustomerAddressRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<ProductCategoryRepository>();
            builder.Services.AddScoped<ProductDescriptionRepository>();
            builder.Services.AddScoped<ProductModelRepository>();
            builder.Services.AddScoped<ProductModelProductDescriptionRepository>();
            builder.Services.AddScoped<SalesOrderDetailRepository>();
            builder.Services.AddScoped<SalesOrderHeaderRepository>();

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

            // 4.1. Views: Popups

            // 4.1.11 Views: BuildVersion Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListQuickActionsPopup>();

            // 4.1.21 Views: ErrorLog Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListQuickActionsPopup>();

            // 4.1.31 Views: Address Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Address.ListQuickActionsPopup>();

            // 4.1.41 Views: Customer Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Customer.ListQuickActionsPopup>();

            // 4.1.51 Views: CustomerAddress Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListQuickActionsPopup>();

            // 4.1.61 Views: Product Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.Product.ListQuickActionsPopup>();

            // 4.1.71 Views: ProductCategory Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListQuickActionsPopup>();

            // 4.1.81 Views: ProductDescription Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListQuickActionsPopup>();

            // 4.1.91 Views: ProductModel Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListQuickActionsPopup>();

            // 4.1.101 Views: ProductModelProductDescription Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListQuickActionsPopup>();

            // 4.1.111 Views: SalesOrderDetail Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListQuickActionsPopup>();

            // 4.1.121 Views: SalesOrderHeader Popups
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.AdvancedSearchPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.CreatePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DeletePopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DetailsPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.EditPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListOrderBysPopup>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListQuickActionsPopup>();

            return builder.Build();
        }
    }
}

