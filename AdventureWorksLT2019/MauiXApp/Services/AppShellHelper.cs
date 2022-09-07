using AdventureWorksLT2019.MauiXApp.Pages;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public static class AppShellHelper
    {
        //public static Dictionary<string, Type> GetRoutes()
        //{
        //    var routes = new Dictionary<string, Type>();
        //    //routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage));
        //    //routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.RegisterUserPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.RegisterUserPage));
        //    //routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage));
        //    routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage));
        //    routes.Add(nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage), typeof(AdventureWorksLT2019.MauiXApp.Pages.MainPage));
        //    return routes;
        //}

        public async static Task AddFlyoutMenusDetails(bool gotoFirstTimeUserPage)
        {
            string gotoRoute = string.Empty;
            // 1. Main Page
            {
                var route = nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage);
                var pageType = typeof(AdventureWorksLT2019.MauiXApp.Pages.MainPage);
                var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
                if (thePage != null) AppShell.Current.Items.Remove(thePage);

                var flyoutItem = new FlyoutItem()
                {
                    Title = AdventureWorksLT2019.Resx.Resources.UIStrings.Home,
                    Route = route,
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Title = AdventureWorksLT2019.Resx.Resources.UIStrings.Home,
                            ContentTemplate = new DataTemplate(pageType),
                        },
                    }
                };
                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                }

                if (!gotoFirstTimeUserPage)
                {
                    gotoRoute = route;
                }
            }

            // 1. Main Page
            {
                var route = nameof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage);
                var pageType = typeof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage);
                var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
                if (thePage != null) AppShell.Current.Items.Remove(thePage);

                var flyoutItem = new FlyoutItem()
                {
                    Title = AdventureWorksLT2019.Resx.Resources.UIStrings.FirstTimeUser,
                    Route = route,
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Title = AdventureWorksLT2019.Resx.Resources.UIStrings.FirstTimeUser,
                            ContentTemplate = new DataTemplate(pageType),
                        },
                    }
                };
                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                }

                if (gotoFirstTimeUserPage)
                {
                    gotoRoute = route;
                }
            }

            await GoToAbsoluteAsync(gotoRoute);
        }

        public static async Task GoToAbsoluteAsync(string route)
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                AppShell.Current.Dispatcher.Dispatch(async () =>
                {
                    await Shell.Current.GoToAsync($"//{route}");
                });
            }
            else
            {
                await Shell.Current.GoToAsync($"//{route}");
            }
        }

        public static async Task GoToRelativeAsync(string route)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync(route);
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync(route);
                }
            }
            catch
            {
                await GoToAbsoluteAsync(route);
            }
        }
    }
}
