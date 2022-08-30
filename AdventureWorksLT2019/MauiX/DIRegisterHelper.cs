namespace AdventureWorksLT2019.MauiX
{
    public static class DIRegisterHelper
    {
        public static void RegisterViewModels(MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<AdventureWorksLT2019.MauiX.ViewModels.AppVM>();
        }
    }
}

