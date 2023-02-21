using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp;
using Framework.Models;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Common.Helpers;

public static class LaunchViewCommandsHelper
{

    //1. BuildVersion

    // 1.8. GetLaunchBuildVersionDetailsPopupCommand
    public static Command<byte> GetLaunchBuildVersionDetailsPopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 1.12. GetLaunchBuildVersionListOrderBysPopupCommand
    public static Command GetLaunchBuildVersionListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //2. ErrorLog

    // 2.8. GetLaunchErrorLogDetailsPopupCommand
    public static Command<int> GetLaunchErrorLogDetailsPopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 2.12. GetLaunchErrorLogListOrderBysPopupCommand
    public static Command GetLaunchErrorLogListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //3. Address

    // 3.5. GetLaunchAddressDashboardPageCommand
    public static Command GetLaunchAddressDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 3.6. GetLaunchAddressCreatePopupCommand
    public static Command GetLaunchAddressCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 3.8. GetLaunchAddressDetailsPopupCommand
    public static Command<int> GetLaunchAddressDetailsPopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 3.9. GetLaunchAddressEditPopupCommand
    public static Command<int> GetLaunchAddressEditPopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 3.12. GetLaunchAddressListOrderBysPopupCommand
    public static Command GetLaunchAddressListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //4. Customer

    // 4.5. GetLaunchCustomerDashboardPageCommand
    public static Command GetLaunchCustomerDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 4.6. GetLaunchCustomerCreatePopupCommand
    public static Command GetLaunchCustomerCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 4.8. GetLaunchCustomerDetailsPopupCommand
    public static Command<int> GetLaunchCustomerDetailsPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 4.9. GetLaunchCustomerEditPopupCommand
    public static Command<int> GetLaunchCustomerEditPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 4.12. GetLaunchCustomerListOrderBysPopupCommand
    public static Command GetLaunchCustomerListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //5. CustomerAddress

    // 5.6. GetLaunchCustomerAddressCreatePopupCommand
    public static Command GetLaunchCustomerAddressCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 5.8. GetLaunchCustomerAddressDetailsPopupCommand
    public static Command<int> GetLaunchCustomerAddressDetailsPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 5.9. GetLaunchCustomerAddressEditPopupCommand
    public static Command<int> GetLaunchCustomerAddressEditPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 5.12. GetLaunchCustomerAddressListOrderBysPopupCommand
    public static Command GetLaunchCustomerAddressListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //6. Product

    // 6.5. GetLaunchProductDashboardPageCommand
    public static Command GetLaunchProductDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 6.6. GetLaunchProductCreatePopupCommand
    public static Command GetLaunchProductCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 6.8. GetLaunchProductDetailsPopupCommand
    public static Command<int> GetLaunchProductDetailsPopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 6.9. GetLaunchProductEditPopupCommand
    public static Command<int> GetLaunchProductEditPopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 6.12. GetLaunchProductListOrderBysPopupCommand
    public static Command GetLaunchProductListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //7. ProductCategory

    // 7.5. GetLaunchProductCategoryDashboardPageCommand
    public static Command GetLaunchProductCategoryDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 7.6. GetLaunchProductCategoryCreatePopupCommand
    public static Command GetLaunchProductCategoryCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 7.8. GetLaunchProductCategoryDetailsPopupCommand
    public static Command<int> GetLaunchProductCategoryDetailsPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 7.9. GetLaunchProductCategoryEditPopupCommand
    public static Command<int> GetLaunchProductCategoryEditPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 7.12. GetLaunchProductCategoryListOrderBysPopupCommand
    public static Command GetLaunchProductCategoryListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //8. ProductDescription

    // 8.5. GetLaunchProductDescriptionDashboardPageCommand
    public static Command GetLaunchProductDescriptionDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 8.6. GetLaunchProductDescriptionCreatePopupCommand
    public static Command GetLaunchProductDescriptionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 8.8. GetLaunchProductDescriptionDetailsPopupCommand
    public static Command<int> GetLaunchProductDescriptionDetailsPopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 8.9. GetLaunchProductDescriptionEditPopupCommand
    public static Command<int> GetLaunchProductDescriptionEditPopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 8.12. GetLaunchProductDescriptionListOrderBysPopupCommand
    public static Command GetLaunchProductDescriptionListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //9. ProductModel

    // 9.5. GetLaunchProductModelDashboardPageCommand
    public static Command GetLaunchProductModelDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 9.6. GetLaunchProductModelCreatePopupCommand
    public static Command GetLaunchProductModelCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 9.8. GetLaunchProductModelDetailsPopupCommand
    public static Command<int> GetLaunchProductModelDetailsPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 9.9. GetLaunchProductModelEditPopupCommand
    public static Command<int> GetLaunchProductModelEditPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 9.12. GetLaunchProductModelListOrderBysPopupCommand
    public static Command GetLaunchProductModelListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //10. ProductModelProductDescription

    // 10.6. GetLaunchProductModelProductDescriptionCreatePopupCommand
    public static Command GetLaunchProductModelProductDescriptionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 10.8. GetLaunchProductModelProductDescriptionDetailsPopupCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDetailsPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 10.9. GetLaunchProductModelProductDescriptionEditPopupCommand
    public static Command<int> GetLaunchProductModelProductDescriptionEditPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 10.12. GetLaunchProductModelProductDescriptionListOrderBysPopupCommand
    public static Command GetLaunchProductModelProductDescriptionListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //11. SalesOrderDetail

    // 11.6. GetLaunchSalesOrderDetailCreatePopupCommand
    public static Command GetLaunchSalesOrderDetailCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 11.8. GetLaunchSalesOrderDetailDetailsPopupCommand
    public static Command<int> GetLaunchSalesOrderDetailDetailsPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 11.9. GetLaunchSalesOrderDetailEditPopupCommand
    public static Command<int> GetLaunchSalesOrderDetailEditPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 11.12. GetLaunchSalesOrderDetailListOrderBysPopupCommand
    public static Command GetLaunchSalesOrderDetailListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //12. SalesOrderHeader

    // 12.5. GetLaunchSalesOrderHeaderDashboardPageCommand
    public static Command GetLaunchSalesOrderHeaderDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 12.6. GetLaunchSalesOrderHeaderCreatePopupCommand
    public static Command GetLaunchSalesOrderHeaderCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 12.8. GetLaunchSalesOrderHeaderDetailsPopupCommand
    public static Command<int> GetLaunchSalesOrderHeaderDetailsPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DetailsPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 12.9. GetLaunchSalesOrderHeaderEditPopupCommand
    public static Command<int> GetLaunchSalesOrderHeaderEditPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 12.12. GetLaunchSalesOrderHeaderListOrderBysPopupCommand
    public static Command GetLaunchSalesOrderHeaderListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }
}

