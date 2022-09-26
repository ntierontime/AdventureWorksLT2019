using AdventureWorksLT2019.MauiXApp.Views;
using AdventureWorksLT2019.Resx.Resources;

using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.Common.Services;

public class AppShellService
{
    public static ICommand ShellGotoAbsoluteCommand { get; protected set; } = new Command<string>(OnShellGotoAbsolute);

    private readonly UserService _userService;

    public AppShellService(
        UserService userService)
    {
        _userService = userService;
    }

    protected static async void OnShellGotoAbsolute(string route)
    {
        await AppShellService.GoToAbsoluteAsync(route);
    }

    public async Task AddFlyoutMenus(bool gotoFirstTimeUserPage)
    {
        string gotoRoute = string.Empty;
        // 1. Main Page
        {
            var route = nameof(MainPage);
            AddFlyoutItem(
                route,
                typeof(MainPage),
                UIStrings.Home);

            if (!gotoFirstTimeUserPage)
            {
                gotoRoute = route;
            }
        }

        // 2. FirstTimeUserPage
        {
            var route = nameof(FirstTimeUserPage);
            AddFlyoutItem(
                route,
                typeof(FirstTimeUserPage),
                UIStrings.FirstTimeUser);

            if (gotoFirstTimeUserPage)
            {
                gotoRoute = route;
            }
        }

        // TODO: For Testing Purpose: table relate ListPage
        AddListPagesToFlyoutItems();

        // SettingsPage
        AddFlyoutItem(
            nameof(SettingsPage),
            typeof(SettingsPage),
            UIStrings.Settings);

        var logoutMenuItem = new MenuItem
        {
            Text = UIStrings.LogOut,
            Command = new Command(async () =>
            {
                var succeeded = await _userService.LogOutAsync();
                if (succeeded)
                {
                    await GoToAbsoluteAsync(nameof(LogInPage));
                }
            })

        };
        AppShell.Current.Items.Add(logoutMenuItem);

        // TODO: For Testing Purpose: table relate Create/Delete/Details/Edit page
        AddCRUDPagesShellContents();

        // TODO: for testing purpose
        gotoRoute = AppShellRoutes.ProductListPage;

        if (!string.IsNullOrEmpty(gotoRoute))
        {
            await GoToAbsoluteAsync(gotoRoute);
        }
    }

    /// <summary>
    /// in flyout menu
    /// </summary>
    public static void AddListPagesToFlyoutItems()
    {
        AddFlyoutItem(AppShellRoutes.BuildVersionListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListPage), UIStrings.BuildVersion);
        AddFlyoutItem(AppShellRoutes.ErrorLogListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListPage), UIStrings.ErrorLog);
        AddFlyoutItem(AppShellRoutes.AddressListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Address.ListPage), UIStrings.Address);
        AddFlyoutItem(AppShellRoutes.CustomerListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.ListPage), UIStrings.Customer);
        AddFlyoutItem(AppShellRoutes.CustomerAddressListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListPage), UIStrings.CustomerAddress);
        AddFlyoutItem(AppShellRoutes.ProductListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Product.ListPage), UIStrings.Product);
        AddFlyoutItem(AppShellRoutes.ProductCategoryListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListPage), UIStrings.ProductCategory);
        AddFlyoutItem(AppShellRoutes.ProductDescriptionListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListPage), UIStrings.ProductDescription);
        AddFlyoutItem(AppShellRoutes.ProductModelListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListPage), UIStrings.ProductModel);
        AddFlyoutItem(AppShellRoutes.ProductModelProductDescriptionListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListPage), UIStrings.ProductModelProductDescription);
        AddFlyoutItem(AppShellRoutes.SalesOrderDetailListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListPage), UIStrings.SalesOrderDetail);
        AddFlyoutItem(AppShellRoutes.SalesOrderHeaderListPage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListPage), UIStrings.SalesOrderHeader);
    }

    /// <summary>
    /// Not in flyout menu
    /// </summary>
    public static void AddCRUDPagesShellContents()
    {

    }

    // this is to add table related List Page
    private static void AddFlyoutItem(string tableName, string route, Type pageType, string title)
    {
        AddFlyoutItem(tableName + route, pageType, title);
    }

    private static void AddShellContent(bool flyoutItemIsVisible, string route, Type pageType, string title, FlyoutBehavior flyoutBehavior = FlyoutBehavior.Disabled)
    {
        var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
        if (thePage != null) AppShell.Current.Items.Remove(thePage);

        var shellContent = new ShellContent
        {
            FlyoutItemIsVisible = flyoutItemIsVisible,
            Route = route,
            ContentTemplate = new DataTemplate(pageType),
            Title = title
        };
        if (!AppShell.Current.Items.Contains(shellContent))
        {
            AppShell.Current.Items.Add(shellContent);
        }
        // ((AppShell)shellContent.Parent).FlyoutBehavior = flyoutBehavior;
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

