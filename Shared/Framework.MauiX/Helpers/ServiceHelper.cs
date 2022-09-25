namespace Framework.MauiX.Helpers;

public static class ServiceHelper
{
    public static TService GetService<TService>() => Current.GetService<TService>();
    public static object GetService(Type type) => Current.GetService(type);

    public static IServiceProvider Current =>
#if WINDOWS
        MauiWinUIApplication.Current.Services;
#elif ANDROID
        MauiApplication.Current.Services;
#elif IOS || MACCATALYST
        MauiUIApplicationDelegate.Current.Services;
#else
        null;
#endif
}

