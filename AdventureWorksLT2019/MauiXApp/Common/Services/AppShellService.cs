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
        //AddCRUDPagesShellContents();

        // TODO: for testing purpose
        gotoRoute = AppShellRoutes.AddressListPage;

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
        // 1. BuildVersion
        AddShellContent(false, AppShellRoutes.BuildVersionCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.BuildVersion.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.BuildVersionDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.BuildVersionDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.BuildVersionEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.BuildVersion.EditPage), UIStrings.Edit);
        // 2. ErrorLog
        AddShellContent(false, AppShellRoutes.ErrorLogCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ErrorLog.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.ErrorLogDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.ErrorLogDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.ErrorLogEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ErrorLog.EditPage), UIStrings.Edit);
        // 3. Address
        AddShellContent(false, AppShellRoutes.AddressCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Address.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.AddressDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Address.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.AddressDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Address.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.AddressEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Address.EditPage), UIStrings.Edit);
        // 4. Customer
        AddShellContent(false, AppShellRoutes.CustomerCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.CustomerDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.CustomerDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.CustomerEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Customer.EditPage), UIStrings.Edit);
        // 5. CustomerAddress
        AddShellContent(false, AppShellRoutes.CustomerAddressCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.CustomerAddressDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.CustomerAddressDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.CustomerAddressEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.EditPage), UIStrings.Edit);
        // 6. Product
        AddShellContent(false, AppShellRoutes.ProductCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Product.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.ProductDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.Product.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.ProductDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Product.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.ProductEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.Product.EditPage), UIStrings.Edit);
        // 7. ProductCategory
        AddShellContent(false, AppShellRoutes.ProductCategoryCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductCategory.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.ProductCategoryDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.ProductCategoryDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.ProductCategoryEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductCategory.EditPage), UIStrings.Edit);
        // 8. ProductDescription
        AddShellContent(false, AppShellRoutes.ProductDescriptionCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductDescription.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.ProductDescriptionDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.ProductDescriptionDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.ProductDescriptionEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductDescription.EditPage), UIStrings.Edit);
        // 9. ProductModel
        AddShellContent(false, AppShellRoutes.ProductModelCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModel.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.ProductModelDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModel.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.ProductModelDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModel.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.ProductModelEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModel.EditPage), UIStrings.Edit);
        // 10. ProductModelProductDescription
        AddShellContent(false, AppShellRoutes.ProductModelProductDescriptionCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.ProductModelProductDescriptionDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.ProductModelProductDescriptionDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.ProductModelProductDescriptionEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.EditPage), UIStrings.Edit);
        // 11. SalesOrderDetail
        AddShellContent(false, AppShellRoutes.SalesOrderDetailCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.SalesOrderDetailDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.SalesOrderDetailDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.SalesOrderDetailEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.EditPage), UIStrings.Edit);
        // 12. SalesOrderHeader
        AddShellContent(false, AppShellRoutes.SalesOrderHeaderCreatePage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.CreatePage), UIStrings.Create_New);
        AddShellContent(false, AppShellRoutes.SalesOrderHeaderDeletePage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DeletePage), UIStrings.Delete);
        AddShellContent(false, AppShellRoutes.SalesOrderHeaderDetailsPage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DetailsPage), UIStrings.Details);
        AddShellContent(false, AppShellRoutes.SalesOrderHeaderEditPage, typeof(AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.EditPage), UIStrings.Edit);
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

