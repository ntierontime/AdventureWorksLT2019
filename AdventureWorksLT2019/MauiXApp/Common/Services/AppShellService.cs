using AdventureWorksLT2019.MauiXApp.Views.Common;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Common.Services;

public class AppShellService
{
    public static ICommand ShellGotoAbsoluteCommand { get; protected set; } = new Command<string>(OnShellGotoAbsolute);

    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.UserService _userService;

    public AppShellService(
        AdventureWorksLT2019.MauiXApp.Common.Services.UserService userService)
    {
        _userService = userService;
    }

    protected static async void OnShellGotoAbsolute(string route)
    {
        await AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService.GoToAbsoluteAsync(route);
    }

    public async Task AddFlyoutMenus(bool gotoFirstTimeUserPage)
    {
        string gotoRoute = string.Empty;
        // 1. Main Page
        {
            var route = nameof(AdventureWorksLT2019.MauiXApp.Views.MainPage);
            AddFlyoutItem(
                route,
                typeof(AdventureWorksLT2019.MauiXApp.Views.MainPage),
                AdventureWorksLT2019.Resx.Resources.UIStrings.Home);

            if (!gotoFirstTimeUserPage)
            {
                gotoRoute = route;
            }
        }

        // 2. FirstTimeUserPage
        {
            var route = nameof(AdventureWorksLT2019.MauiXApp.Views.FirstTimeUserPage);
            AddFlyoutItem(
                route,
                typeof(AdventureWorksLT2019.MauiXApp.Views.FirstTimeUserPage),
                AdventureWorksLT2019.Resx.Resources.UIStrings.FirstTimeUser);

            if (gotoFirstTimeUserPage)
            {
                gotoRoute = route;
            }
        }

        // table relate ListPage
        AddFlyoutItems();

        // SettingsPage
        AddFlyoutItem(
            nameof(AdventureWorksLT2019.MauiXApp.Views.Common.SettingsPage),
            typeof(AdventureWorksLT2019.MauiXApp.Views.Common.SettingsPage),
            AdventureWorksLT2019.Resx.Resources.UIStrings.Settings);

        var logoutMenuItem = new MenuItem
        {
            Text = AdventureWorksLT2019.Resx.Resources.UIStrings.LogOut,
            Command = new Command(async () =>
            {
                var succeeded = await _userService.LogOutAsync();
                if (succeeded)
                {
                    await GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.LogInPage));
                }
            })

        };
        AppShell.Current.Items.Add(logoutMenuItem);

        RegisterRoutes();

        // TODO: for testing purpose
        gotoRoute = AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerListPage;

        if (!string.IsNullOrEmpty(gotoRoute))
        {
            await GoToAbsoluteAsync(gotoRoute);
        }
    }


    /// <summary>
    /// Not in flyout menu
    /// </summary>
    public static void AddFlyoutItems()
    {
        AddFlyoutItem(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.ListPage), AdventureWorksLT2019.Resx.Resources.UIStrings.Customer);
    }

    /// <summary>
    /// Not in flyout menu
    /// </summary>
    public static void RegisterRoutes()
    {
        Routing.RegisterRoute(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePage));
        Routing.RegisterRoute(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.EditPage));
        Routing.RegisterRoute(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePage));
        Routing.RegisterRoute(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.CustomerDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPage));
    }

    // this is to add table related List Page
    private static void AddFlyoutItem(string tableName, string route, Type pageType, string title)
    {
        AddFlyoutItem(tableName + route, pageType, title);
    }

    private static void AddShellContent(bool flyoutItemIsVisible, string route, Type pageType, string title)
    {
        var shellContent = new ShellContent
        {
            FlyoutItemIsVisible = flyoutItemIsVisible,
            Route = route,
            ContentTemplate = new DataTemplate(pageType),
            Title = title,
        };
        AppShell.Current.Items.Add(shellContent);
    }

    private static void AddFlyoutItem(string route, Type pageType, string title)
    {
        var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
        if (thePage != null) AppShell.Current.Items.Remove(thePage);

        var flyoutItem = new FlyoutItem()
        {
            Title = title,
            Route = route,
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
            Items =
                {
                    new ShellContent
                    {
                        Title = title,
                        ContentTemplate = new DataTemplate(pageType),
                    },
                }
        };
        if (!AppShell.Current.Items.Contains(flyoutItem))
        {
            AppShell.Current.Items.Add(flyoutItem);
        }
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
