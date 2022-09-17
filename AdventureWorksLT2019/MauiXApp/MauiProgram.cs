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
                .UseMauiApp<AdventureWorksLT2019.MauiXApp.App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("MaterialIcons-Regular.ttf", Framework.MauiX.Icons.MaterialIconFamilies.MaterialIconRegular);
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", Framework.MauiX.Icons.MaterialIconFamilies.MaterialIconOutlined);
                    fonts.AddFont("MaterialIconsRound-Regular.otf", Framework.MauiX.Icons.MaterialIconFamilies.MaterialIconRound);
                    fonts.AddFont("MaterialIconsSharp-Regular.otf", Framework.MauiX.Icons.MaterialIconFamilies.MaterialIconSharp);
                    fonts.AddFont("MaterialIconsTwoTone-Regular.otf", Framework.MauiX.Icons.MaterialIconFamilies.MaterialIconTwoTone);
                });

            builder.Services.AddLocalization();

            // 1. Essential
            builder.Services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
            builder.Services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);

            // 2.1. Framework.MauiX.Services
            builder.Services.AddSingleton<Framework.MauiX.Services.SecureStorageService>();
            builder.Services.AddSingleton<Framework.MauiX.SQLite.SQLiteService>();
            builder.Services.AddSingleton<Framework.MauiX.SQLite.CacheDataStatusRepository>();
            builder.Services.AddSingleton<Framework.MauiX.SQLite.CacheDataStatusService>();
            
            // 2.2. AdventureWorksLT2019.MauiX.WebApiClients
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.WebApiClients.WebApiConfig>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.WebApiClients.AuthenticationApiClient>();

            // 2.3. AdventureWorksLT2019.MauiX.Services
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.Services.UserService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.Services.AppLoadingService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.Services.GeoLocationService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService>();

            // 2.4. DataModels
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppStates>();

            // 2.5. ViewModels
            // 2.5.1. Global Page ViewModels
            // 2.5.1.1. AdventureWorksLT2019.MauiXApp.ViewModels.AppVM is a Singleton/Global, most of others are Scoped(new instance)
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppLoadingVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.LogInVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.RegisterUserVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.FirstTimeUserVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.SettingsVM>();

            // 3.1. WebApi Clients
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient>();

            // 3.2. SQLite Repositories - Register if wannt to cache.
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository>();

            // 3.3. Services
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.CustomerService>();

            // 3.4. View Models
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ItemVM>();

            return builder.Build();
        }
    }
}