using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

namespace AdventureWorksLT2019.MauiXApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
#if ANDROID && DEBUG
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddLocalization();

            // 1. Framework.MauiX.Services
            builder.Services.AddSingleton<Framework.MauiX.Services.SecureStorageService>();
            builder.Services.AddScoped<Framework.MauiX.Services.IThemeService, AdventureWorksLT2019.MauiXApp.Services.ThemeService>();

            // 2. AdventureWorksLT2019.MauiX.WebApiClients
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationWebApiConfig>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.WebApiClients.AuthenticationApiClient>();

            // 3. AdventureWorksLT2019.MauiX.Services
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.AuthenticationService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.AppLoadingService>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.Services.GeoLocationService>();

            // 4. DataModels
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.DataModels.AppStates>();

            // 5. ViewModels
            // 5.1. Global Page ViewModels
            // 5.1.1. AdventureWorksLT2019.MauiXApp.ViewModels.AppVM is a Singleton/Global, most of others are Scoped(new instance)
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppShellVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppLoadingVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.LogInVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.RegisterUserVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.FirstTimeUserVM>();

            return builder.Build();
        }
    }
}