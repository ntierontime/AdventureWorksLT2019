namespace AdventureWorksLT2019.MauiXApp.Services
{
    public static class AppShellRoutingHelper
    {
        public static Dictionary<string, Type> GetRoutes()
        {
            var routes = new Dictionary<string, Type>();
            routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage));
            routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.RegisterUserPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.RegisterUserPage));
            routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage));
            routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage));
            routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.MainPage));
            return routes;
        }
    }
}
