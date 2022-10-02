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

    //2. BuildVersion

    // 1.1. GetLaunchBuildVersionCreatePageCommand
    public static Command GetLaunchBuildVersionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionCreatePage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 1.2. GetLaunchBuildVersionDeletePageCommand
    public static Command<byte> GetLaunchBuildVersionDeletePageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDeletePage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 1.3. GetLaunchBuildVersionDetailsPageCommand
    public static Command<byte> GetLaunchBuildVersionDetailsPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDetailsPage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 1.4. GetLaunchBuildVersionEditPageCommand
    public static Command<byte> GetLaunchBuildVersionEditPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionEditPage);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 1.5. GetLaunchBuildVersionCreatePopupCommand
    public static Command GetLaunchBuildVersionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.CreatePopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.6. GetLaunchBuildVersionDeletePopupCommand
    public static Command<byte> GetLaunchBuildVersionDeletePopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DeletePopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.7. GetLaunchBuildVersionDetailsPopupCommand
    public static Command<byte> GetLaunchBuildVersionDetailsPopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DetailsPopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.8. GetLaunchBuildVersionEditPopupCommand
    public static Command<byte> GetLaunchBuildVersionEditPopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.EditPopup();
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.9. GetLaunchBuildVersionAdvancedSearchPopupCommand
    public static Command GetLaunchBuildVersionAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.10. GetLaunchBuildVersionListQuickActionsPopupCommand
    public static Command GetLaunchBuildVersionListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.11. GetLaunchBuildVersionListOrderBysPopupCommand
    public static Command GetLaunchBuildVersionListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //3. ErrorLog

    // 2.1. GetLaunchErrorLogCreatePageCommand
    public static Command GetLaunchErrorLogCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogCreatePage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 2.2. GetLaunchErrorLogDeletePageCommand
    public static Command<int> GetLaunchErrorLogDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDeletePage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 2.3. GetLaunchErrorLogDetailsPageCommand
    public static Command<int> GetLaunchErrorLogDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDetailsPage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 2.4. GetLaunchErrorLogEditPageCommand
    public static Command<int> GetLaunchErrorLogEditPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogEditPage);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 2.5. GetLaunchErrorLogCreatePopupCommand
    public static Command GetLaunchErrorLogCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.CreatePopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.6. GetLaunchErrorLogDeletePopupCommand
    public static Command<int> GetLaunchErrorLogDeletePopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DeletePopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.7. GetLaunchErrorLogDetailsPopupCommand
    public static Command<int> GetLaunchErrorLogDetailsPopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.8. GetLaunchErrorLogEditPopupCommand
    public static Command<int> GetLaunchErrorLogEditPopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.EditPopup();
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.9. GetLaunchErrorLogAdvancedSearchPopupCommand
    public static Command GetLaunchErrorLogAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.10. GetLaunchErrorLogListQuickActionsPopupCommand
    public static Command GetLaunchErrorLogListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.11. GetLaunchErrorLogListOrderBysPopupCommand
    public static Command GetLaunchErrorLogListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //4. Address

    // 3.1. GetLaunchAddressCreatePageCommand
    public static Command GetLaunchAddressCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressCreatePage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 3.2. GetLaunchAddressDeletePageCommand
    public static Command<int> GetLaunchAddressDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDeletePage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 3.3. GetLaunchAddressDetailsPageCommand
    public static Command<int> GetLaunchAddressDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDetailsPage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 3.4. GetLaunchAddressEditPageCommand
    public static Command<int> GetLaunchAddressEditPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressEditPage);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 3.5. GetLaunchAddressCreatePopupCommand
    public static Command GetLaunchAddressCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.CreatePopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.6. GetLaunchAddressDeletePopupCommand
    public static Command<int> GetLaunchAddressDeletePopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.DeletePopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.7. GetLaunchAddressDetailsPopupCommand
    public static Command<int> GetLaunchAddressDetailsPopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.DetailsPopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.8. GetLaunchAddressEditPopupCommand
    public static Command<int> GetLaunchAddressEditPopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.EditPopup();
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.9. GetLaunchAddressAdvancedSearchPopupCommand
    public static Command GetLaunchAddressAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.10. GetLaunchAddressListQuickActionsPopupCommand
    public static Command GetLaunchAddressListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.11. GetLaunchAddressListOrderBysPopupCommand
    public static Command GetLaunchAddressListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //5. Customer

    // 4.1. GetLaunchCustomerCreatePageCommand
    public static Command GetLaunchCustomerCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerCreatePage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 4.2. GetLaunchCustomerDeletePageCommand
    public static Command<int> GetLaunchCustomerDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDeletePage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 4.3. GetLaunchCustomerDetailsPageCommand
    public static Command<int> GetLaunchCustomerDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDetailsPage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 4.4. GetLaunchCustomerEditPageCommand
    public static Command<int> GetLaunchCustomerEditPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerEditPage);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 4.5. GetLaunchCustomerCreatePopupCommand
    public static Command GetLaunchCustomerCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.CreatePopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.6. GetLaunchCustomerDeletePopupCommand
    public static Command<int> GetLaunchCustomerDeletePopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.7. GetLaunchCustomerDetailsPopupCommand
    public static Command<int> GetLaunchCustomerDetailsPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DetailsPopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.8. GetLaunchCustomerEditPopupCommand
    public static Command<int> GetLaunchCustomerEditPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.EditPopup();
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.9. GetLaunchCustomerAdvancedSearchPopupCommand
    public static Command GetLaunchCustomerAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.10. GetLaunchCustomerListQuickActionsPopupCommand
    public static Command GetLaunchCustomerListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.11. GetLaunchCustomerListOrderBysPopupCommand
    public static Command GetLaunchCustomerListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //6. CustomerAddress

    // 5.1. GetLaunchCustomerAddressCreatePageCommand
    public static Command GetLaunchCustomerAddressCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressCreatePage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 5.2. GetLaunchCustomerAddressDeletePageCommand
    public static Command<int> GetLaunchCustomerAddressDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDeletePage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 5.3. GetLaunchCustomerAddressDetailsPageCommand
    public static Command<int> GetLaunchCustomerAddressDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDetailsPage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 5.4. GetLaunchCustomerAddressEditPageCommand
    public static Command<int> GetLaunchCustomerAddressEditPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressEditPage);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 5.5. GetLaunchCustomerAddressCreatePopupCommand
    public static Command GetLaunchCustomerAddressCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.CreatePopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.6. GetLaunchCustomerAddressDeletePopupCommand
    public static Command<int> GetLaunchCustomerAddressDeletePopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DeletePopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.7. GetLaunchCustomerAddressDetailsPopupCommand
    public static Command<int> GetLaunchCustomerAddressDetailsPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DetailsPopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.8. GetLaunchCustomerAddressEditPopupCommand
    public static Command<int> GetLaunchCustomerAddressEditPopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.EditPopup();
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.9. GetLaunchCustomerAddressAdvancedSearchPopupCommand
    public static Command GetLaunchCustomerAddressAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.10. GetLaunchCustomerAddressListQuickActionsPopupCommand
    public static Command GetLaunchCustomerAddressListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.11. GetLaunchCustomerAddressListOrderBysPopupCommand
    public static Command GetLaunchCustomerAddressListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //7. Product

    // 6.1. GetLaunchProductCreatePageCommand
    public static Command GetLaunchProductCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCreatePage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 6.2. GetLaunchProductDeletePageCommand
    public static Command<int> GetLaunchProductDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDeletePage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 6.3. GetLaunchProductDetailsPageCommand
    public static Command<int> GetLaunchProductDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 6.4. GetLaunchProductEditPageCommand
    public static Command<int> GetLaunchProductEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductEditPage);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 6.5. GetLaunchProductCreatePopupCommand
    public static Command GetLaunchProductCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.6. GetLaunchProductDeletePopupCommand
    public static Command<int> GetLaunchProductDeletePopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.7. GetLaunchProductDetailsPopupCommand
    public static Command<int> GetLaunchProductDetailsPopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.8. GetLaunchProductEditPopupCommand
    public static Command<int> GetLaunchProductEditPopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.9. GetLaunchProductAdvancedSearchPopupCommand
    public static Command GetLaunchProductAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.10. GetLaunchProductListQuickActionsPopupCommand
    public static Command GetLaunchProductListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.11. GetLaunchProductListOrderBysPopupCommand
    public static Command GetLaunchProductListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //8. ProductCategory

    // 7.1. GetLaunchProductCategoryCreatePageCommand
    public static Command GetLaunchProductCategoryCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryCreatePage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 7.2. GetLaunchProductCategoryDeletePageCommand
    public static Command<int> GetLaunchProductCategoryDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDeletePage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 7.3. GetLaunchProductCategoryDetailsPageCommand
    public static Command<int> GetLaunchProductCategoryDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 7.4. GetLaunchProductCategoryEditPageCommand
    public static Command<int> GetLaunchProductCategoryEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryEditPage);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 7.5. GetLaunchProductCategoryCreatePopupCommand
    public static Command GetLaunchProductCategoryCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.6. GetLaunchProductCategoryDeletePopupCommand
    public static Command<int> GetLaunchProductCategoryDeletePopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.7. GetLaunchProductCategoryDetailsPopupCommand
    public static Command<int> GetLaunchProductCategoryDetailsPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.8. GetLaunchProductCategoryEditPopupCommand
    public static Command<int> GetLaunchProductCategoryEditPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.9. GetLaunchProductCategoryAdvancedSearchPopupCommand
    public static Command GetLaunchProductCategoryAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.10. GetLaunchProductCategoryListQuickActionsPopupCommand
    public static Command GetLaunchProductCategoryListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.11. GetLaunchProductCategoryListOrderBysPopupCommand
    public static Command GetLaunchProductCategoryListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //9. ProductDescription

    // 8.1. GetLaunchProductDescriptionCreatePageCommand
    public static Command GetLaunchProductDescriptionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionCreatePage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 8.2. GetLaunchProductDescriptionDeletePageCommand
    public static Command<int> GetLaunchProductDescriptionDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDeletePage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 8.3. GetLaunchProductDescriptionDetailsPageCommand
    public static Command<int> GetLaunchProductDescriptionDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 8.4. GetLaunchProductDescriptionEditPageCommand
    public static Command<int> GetLaunchProductDescriptionEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionEditPage);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 8.5. GetLaunchProductDescriptionCreatePopupCommand
    public static Command GetLaunchProductDescriptionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.6. GetLaunchProductDescriptionDeletePopupCommand
    public static Command<int> GetLaunchProductDescriptionDeletePopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.7. GetLaunchProductDescriptionDetailsPopupCommand
    public static Command<int> GetLaunchProductDescriptionDetailsPopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.8. GetLaunchProductDescriptionEditPopupCommand
    public static Command<int> GetLaunchProductDescriptionEditPopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.9. GetLaunchProductDescriptionAdvancedSearchPopupCommand
    public static Command GetLaunchProductDescriptionAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.10. GetLaunchProductDescriptionListQuickActionsPopupCommand
    public static Command GetLaunchProductDescriptionListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.11. GetLaunchProductDescriptionListOrderBysPopupCommand
    public static Command GetLaunchProductDescriptionListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //10. ProductModel

    // 9.1. GetLaunchProductModelCreatePageCommand
    public static Command GetLaunchProductModelCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelCreatePage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 9.2. GetLaunchProductModelDeletePageCommand
    public static Command<int> GetLaunchProductModelDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDeletePage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 9.3. GetLaunchProductModelDetailsPageCommand
    public static Command<int> GetLaunchProductModelDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 9.4. GetLaunchProductModelEditPageCommand
    public static Command<int> GetLaunchProductModelEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelEditPage);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 9.5. GetLaunchProductModelCreatePopupCommand
    public static Command GetLaunchProductModelCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.6. GetLaunchProductModelDeletePopupCommand
    public static Command<int> GetLaunchProductModelDeletePopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.7. GetLaunchProductModelDetailsPopupCommand
    public static Command<int> GetLaunchProductModelDetailsPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.8. GetLaunchProductModelEditPopupCommand
    public static Command<int> GetLaunchProductModelEditPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.9. GetLaunchProductModelAdvancedSearchPopupCommand
    public static Command GetLaunchProductModelAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.10. GetLaunchProductModelListQuickActionsPopupCommand
    public static Command GetLaunchProductModelListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.11. GetLaunchProductModelListOrderBysPopupCommand
    public static Command GetLaunchProductModelListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //11. ProductModelProductDescription

    // 10.1. GetLaunchProductModelProductDescriptionCreatePageCommand
    public static Command GetLaunchProductModelProductDescriptionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionCreatePage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 10.2. GetLaunchProductModelProductDescriptionDeletePageCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDeletePage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 10.3. GetLaunchProductModelProductDescriptionDetailsPageCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDetailsPage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 10.4. GetLaunchProductModelProductDescriptionEditPageCommand
    public static Command<int> GetLaunchProductModelProductDescriptionEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionEditPage);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 10.5. GetLaunchProductModelProductDescriptionCreatePopupCommand
    public static Command GetLaunchProductModelProductDescriptionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.CreatePopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.6. GetLaunchProductModelProductDescriptionDeletePopupCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDeletePopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DeletePopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.7. GetLaunchProductModelProductDescriptionDetailsPopupCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDetailsPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DetailsPopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.8. GetLaunchProductModelProductDescriptionEditPopupCommand
    public static Command<int> GetLaunchProductModelProductDescriptionEditPopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.EditPopup();
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.9. GetLaunchProductModelProductDescriptionAdvancedSearchPopupCommand
    public static Command GetLaunchProductModelProductDescriptionAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.10. GetLaunchProductModelProductDescriptionListQuickActionsPopupCommand
    public static Command GetLaunchProductModelProductDescriptionListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.11. GetLaunchProductModelProductDescriptionListOrderBysPopupCommand
    public static Command GetLaunchProductModelProductDescriptionListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //12. SalesOrderDetail

    // 11.1. GetLaunchSalesOrderDetailCreatePageCommand
    public static Command GetLaunchSalesOrderDetailCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailCreatePage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 11.2. GetLaunchSalesOrderDetailDeletePageCommand
    public static Command<int> GetLaunchSalesOrderDetailDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDeletePage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 11.3. GetLaunchSalesOrderDetailDetailsPageCommand
    public static Command<int> GetLaunchSalesOrderDetailDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDetailsPage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 11.4. GetLaunchSalesOrderDetailEditPageCommand
    public static Command<int> GetLaunchSalesOrderDetailEditPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailEditPage);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 11.5. GetLaunchSalesOrderDetailCreatePopupCommand
    public static Command GetLaunchSalesOrderDetailCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.CreatePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.6. GetLaunchSalesOrderDetailDeletePopupCommand
    public static Command<int> GetLaunchSalesOrderDetailDeletePopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DeletePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.7. GetLaunchSalesOrderDetailDetailsPopupCommand
    public static Command<int> GetLaunchSalesOrderDetailDetailsPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DetailsPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.8. GetLaunchSalesOrderDetailEditPopupCommand
    public static Command<int> GetLaunchSalesOrderDetailEditPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.EditPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.9. GetLaunchSalesOrderDetailAdvancedSearchPopupCommand
    public static Command GetLaunchSalesOrderDetailAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.10. GetLaunchSalesOrderDetailListQuickActionsPopupCommand
    public static Command GetLaunchSalesOrderDetailListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.11. GetLaunchSalesOrderDetailListOrderBysPopupCommand
    public static Command GetLaunchSalesOrderDetailListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    //13. SalesOrderHeader

    // 12.1. GetLaunchSalesOrderHeaderCreatePageCommand
    public static Command GetLaunchSalesOrderHeaderCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderCreatePage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 12.2. GetLaunchSalesOrderHeaderDeletePageCommand
    public static Command<int> GetLaunchSalesOrderHeaderDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDeletePage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 12.3. GetLaunchSalesOrderHeaderDetailsPageCommand
    public static Command<int> GetLaunchSalesOrderHeaderDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDetailsPage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 12.4. GetLaunchSalesOrderHeaderEditPageCommand
    public static Command<int> GetLaunchSalesOrderHeaderEditPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderEditPage);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 12.5. GetLaunchSalesOrderHeaderCreatePopupCommand
    public static Command GetLaunchSalesOrderHeaderCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.CreatePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(null, ViewItemTemplates.Create));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.6. GetLaunchSalesOrderHeaderDeletePopupCommand
    public static Command<int> GetLaunchSalesOrderHeaderDeletePopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DeletePopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.7. GetLaunchSalesOrderHeaderDetailsPopupCommand
    public static Command<int> GetLaunchSalesOrderHeaderDetailsPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DetailsPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.8. GetLaunchSalesOrderHeaderEditPopupCommand
    public static Command<int> GetLaunchSalesOrderHeaderEditPopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.EditPopup();
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit));
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.9. GetLaunchSalesOrderHeaderAdvancedSearchPopupCommand
    public static Command GetLaunchSalesOrderHeaderAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.10. GetLaunchSalesOrderHeaderListQuickActionsPopupCommand
    public static Command GetLaunchSalesOrderHeaderListQuickActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListQuickActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.11. GetLaunchSalesOrderHeaderListOrderBysPopupCommand
    public static Command GetLaunchSalesOrderHeaderListOrderBysPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListOrderBysPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }
}

