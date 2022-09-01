using Microsoft.Maui.Hosting;

namespace AdventureWorksLT2019.MauiXApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            RegisterServices(builder);

            return builder.Build();
        }

        /// <summary>
        /// Services are using mauiAppBuilder.Services.AddSingleton(), to be resolved in Constructor
        /// ViewModels are using DependencyService.Register();
        /// </summary>
        /// <param name="mauiAppBuilder"></param>
        public static void RegisterServices(MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<AdventureWorksLT2019.MauiX.ViewModels.AppVM>();
            mauiAppBuilder.Services.AddSingleton<Framework.MauiX.Helpers.IThemeService, AdventureWorksLT2019.MauiX.Services.ThemeService>();
        }
    }
}