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

    public static Command GetLaunchBuildVersionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionCreatePage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<byte> GetLaunchBuildVersionDeletePageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDeletePage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<byte> GetLaunchBuildVersionDetailsPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDetailsPage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<byte> GetLaunchBuildVersionEditPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionEditPage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchBuildVersionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.CreatePopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<byte> GetLaunchBuildVersionDeletePopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DeletePopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<byte> GetLaunchBuildVersionDetailsPopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DetailsPopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<byte> GetLaunchBuildVersionEditPopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.EditPopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //2. ErrorLog

    public static Command GetLaunchErrorLogCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogCreatePage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchErrorLogDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDeletePage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchErrorLogDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDetailsPage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchErrorLogEditPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogEditPage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchErrorLogCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.CreatePopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchErrorLogDeletePopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DeletePopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchErrorLogDetailsPopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchErrorLogEditPopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.EditPopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //3. Address

    public static Command GetLaunchAddressCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressCreatePage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchAddressDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDeletePage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchAddressDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDetailsPage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchAddressEditPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressEditPage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchAddressCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.CreatePopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchAddressDeletePopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.DeletePopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchAddressDetailsPopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.DetailsPopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchAddressEditPopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.EditPopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //4. Customer

    public static Command GetLaunchCustomerCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerCreatePage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchCustomerDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDeletePage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchCustomerDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDetailsPage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchCustomerEditPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerEditPage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchCustomerCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchCustomerDeletePopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchCustomerDetailsPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchCustomerEditPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.EditPopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //5. CustomerAddress

    public static Command GetLaunchCustomerAddressCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressCreatePage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchCustomerAddressDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDeletePage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchCustomerAddressDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDetailsPage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchCustomerAddressEditPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressEditPage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchCustomerAddressCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.CreatePopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchCustomerAddressDeletePopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DeletePopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchCustomerAddressDetailsPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DetailsPopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchCustomerAddressEditPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.EditPopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //6. Product

    public static Command GetLaunchProductCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCreatePage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchProductDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDeletePage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchProductDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchProductEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductEditPage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchProductCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductDeletePopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductDetailsPopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductEditPopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //7. ProductCategory

    public static Command GetLaunchProductCategoryCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryCreatePage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchProductCategoryDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDeletePage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchProductCategoryDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchProductCategoryEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryEditPage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchProductCategoryCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductCategoryDeletePopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductCategoryDetailsPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductCategoryEditPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //8. ProductDescription

    public static Command GetLaunchProductDescriptionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionCreatePage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchProductDescriptionDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDeletePage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchProductDescriptionDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchProductDescriptionEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionEditPage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchProductDescriptionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductDescriptionDeletePopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductDescriptionDetailsPopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductDescriptionEditPopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //9. ProductModel

    public static Command GetLaunchProductModelCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelCreatePage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchProductModelDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDeletePage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchProductModelDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchProductModelEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelEditPage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchProductModelCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductModelDeletePopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductModelDetailsPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductModelEditPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //10. ProductModelProductDescription

    public static Command GetLaunchProductModelProductDescriptionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionCreatePage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchProductModelProductDescriptionDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDeletePage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchProductModelProductDescriptionDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchProductModelProductDescriptionEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionEditPage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchProductModelProductDescriptionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductModelProductDescriptionDeletePopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductModelProductDescriptionDetailsPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductModelProductDescriptionEditPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //11. SalesOrderDetail

    public static Command GetLaunchSalesOrderDetailCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailCreatePage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchSalesOrderDetailDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDeletePage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchSalesOrderDetailDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDetailsPage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchSalesOrderDetailEditPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailEditPage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchSalesOrderDetailCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.CreatePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchSalesOrderDetailDeletePopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DeletePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchSalesOrderDetailDetailsPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DetailsPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchSalesOrderDetailEditPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.EditPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //12. SalesOrderHeader

    public static Command GetLaunchSalesOrderHeaderCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderCreatePage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    public static Command<int> GetLaunchSalesOrderHeaderDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDeletePage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    public static Command<int> GetLaunchSalesOrderHeaderDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDetailsPage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details, returnPath));
        });
    }

    public static Command<int> GetLaunchSalesOrderHeaderEditPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderEditPage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    public static Command GetLaunchSalesOrderHeaderCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.CreatePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchSalesOrderHeaderDeletePopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DeletePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchSalesOrderHeaderDetailsPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DetailsPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchSalesOrderHeaderEditPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.EditPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }
}

