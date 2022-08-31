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

            // builder.Services.AddSingleton<AdventureWorksLT2019.MauiX.ViewModels.AppVM>();
            AdventureWorksLT2019.MauiX.DiIocRegisterHelper.RegisterViewModels(builder);

            return builder.Build();
        }
    }
}