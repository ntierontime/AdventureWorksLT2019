namespace AdventureWorksLT2019.MauiX
{
    public static class DiIocRegisterHelper
    {
        public static void RegisterViewModels(MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<Framework.MauiX.Helpers.IThemesHelper, AdventureWorksLT2019.MauiX.Themes.ThemesHelper>();
            mauiAppBuilder.Services.AddSingleton<Framework.MauiX.ViewModels.ThemeSelectorVM>();
            mauiAppBuilder.Services.AddSingleton<AdventureWorksLT2019.MauiX.ViewModels.AppVM>();
        }
    }
}

