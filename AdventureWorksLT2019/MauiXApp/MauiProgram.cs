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

            // 1.1. Framework.MauiX.Services
            builder.Services.AddSingleton<Framework.MauiX.Services.SecureStorageService>();
            builder.Services.AddScoped<Framework.MauiX.Services.IThemeService, AdventureWorksLT2019.MauiXApp.Services.Common.ThemeService>();
            builder.Services.AddSingleton<Framework.MauiX.SQLite.SQLiteService>();
            builder.Services.AddSingleton<Framework.MauiX.SQLite.CacheDataStatusRepository>();
            builder.Services.AddSingleton<Framework.MauiX.SQLite.CacheDataStatusService>();
            
            // 1.2. AdventureWorksLT2019.MauiX.WebApiClients
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.WebApiClients.Common.WebApiConfig>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.WebApiClients.Common.AuthenticationApiClient>();

            // 1.3. AdventureWorksLT2019.MauiX.Services
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.Common.AuthenticationService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.Common.UserService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.Common.AppLoadingService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.Common.GeoLocationService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.Common.AppShellService>();

            // 1.4. DataModels
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Common.AppStates>();

            // 1.5. ViewModels
            // 1.5.1. Global Page ViewModels
            // 1.5.1.1. AdventureWorksLT2019.MauiXApp.ViewModels.Common.AppVM is a Singleton/Global, most of others are Scoped(new instance)
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Common.AppVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.Common.AppLoadingVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.Common.LogInVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.Common.RegisterUserVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.Common.FirstTimeUserVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.Common.SettingsVM>();

            // 2.1. WebApi Clients
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient>();

            // 2.2. SQLite Repositories - Register if wannt to cache.
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository>();

            // 2.3. Services
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.CustomerService>();

            // 2.4. View Models
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.CustomerListVM>();

            return builder.Build();
        }
    }
}