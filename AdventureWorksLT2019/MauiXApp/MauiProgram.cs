﻿using CommunityToolkit.Maui;
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
            builder.Services.AddScoped<Framework.MauiX.Services.IThemeService, AdventureWorksLT2019.MauiX.Services.ThemeService>();

            // 2. AdventureWorksLT2019.MauiX.WebApiClients
            builder.Services.AddScoped<AdventureWorksLT2019.MauiX.WebApiClients.AuthenticationWebApiConfig>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiX.WebApiClients.AuthenticationApiClient>();

            // 3. AdventureWorksLT2019.MauiX.Services
            builder.Services.AddScoped<AdventureWorksLT2019.MauiX.Services.AuthenticationService>();

            // 4. ViewModels
            // 4.1. Control ViewModels
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.ProgressBarVM>();

            // 4.2. Page ViewModels
            // 4.2.1. AdventureWorksLT2019.MauiXApp.ViewModels.AppVM is a Singleton/Global, most of others are Scoped(new instance)
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppVM>();
            builder.Services.AddSingleton<AdventureWorksLT2019.MauiXApp.ViewModels.AppShellVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.LogInVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.RegisterUserVM>();
            builder.Services.AddScoped<AdventureWorksLT2019.MauiXApp.ViewModels.FirstTimeUserVM>();

            return builder.Build();
        }
    }
}