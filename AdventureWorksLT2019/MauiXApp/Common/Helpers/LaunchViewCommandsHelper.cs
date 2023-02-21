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

    // 1.1. GetLaunchBuildVersionCreatePageCommand
    public static Command GetLaunchBuildVersionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 1.2. GetLaunchBuildVersionDeletePageCommand
    public static Command<byte> GetLaunchBuildVersionDeletePageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 1.3. GetLaunchBuildVersionDetailsPageCommand
    public static Command<byte> GetLaunchBuildVersionDetailsPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 1.4. GetLaunchBuildVersionEditPageCommand
    public static Command<byte> GetLaunchBuildVersionEditPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 1.5. GetLaunchBuildVersionDashboardPageCommand
    public static Command GetLaunchBuildVersionDashboardPageCommand(string returnPath)
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.BuildVersionDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 1.6. GetLaunchBuildVersionCreatePopupCommand
    public static Command GetLaunchBuildVersionCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 1.7. GetLaunchBuildVersionDeletePopupCommand
    public static Command<byte> GetLaunchBuildVersionDeletePopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Delete));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

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

    // 1.9. GetLaunchBuildVersionEditPopupCommand
    public static Command<byte> GetLaunchBuildVersionEditPopupCommand()
    {
        return new Command<byte>(async (systemInformationID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<BuildVersionIdentifierMessage>(new BuildVersionIdentifierMessage(new BuildVersionIdentifier { SystemInformationID = systemInformationID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 1.10. GetLaunchBuildVersionAdvancedSearchPopupCommand
    public static Command GetLaunchBuildVersionAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 1.11. GetLaunchBuildVersionListBulkActionsPopupCommand
    public static Command GetLaunchBuildVersionListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.BuildVersion.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 2.1. GetLaunchErrorLogCreatePageCommand
    public static Command GetLaunchErrorLogCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 2.2. GetLaunchErrorLogDeletePageCommand
    public static Command<int> GetLaunchErrorLogDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 2.3. GetLaunchErrorLogDetailsPageCommand
    public static Command<int> GetLaunchErrorLogDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 2.4. GetLaunchErrorLogEditPageCommand
    public static Command<int> GetLaunchErrorLogEditPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 2.5. GetLaunchErrorLogDashboardPageCommand
    public static Command GetLaunchErrorLogDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (errorLogID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ErrorLogDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

    // 2.6. GetLaunchErrorLogCreatePopupCommand
    public static Command GetLaunchErrorLogCreatePopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.CreatePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(null, ViewItemTemplates.Create));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 2.7. GetLaunchErrorLogDeletePopupCommand
    public static Command<int> GetLaunchErrorLogDeletePopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Delete));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

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

    // 2.9. GetLaunchErrorLogEditPopupCommand
    public static Command<int> GetLaunchErrorLogEditPopupCommand()
    {
        return new Command<int>(async (errorLogID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.EditPopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ErrorLogIdentifierMessage>(new ErrorLogIdentifierMessage(new ErrorLogIdentifier { ErrorLogID = errorLogID }, ViewItemTemplates.Edit));
            AppShell.Current.CurrentPage.ShowPopup(popup);
        });
    }

    // 2.10. GetLaunchErrorLogAdvancedSearchPopupCommand
    public static Command GetLaunchErrorLogAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 2.11. GetLaunchErrorLogListBulkActionsPopupCommand
    public static Command GetLaunchErrorLogListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ErrorLog.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 3.1. GetLaunchAddressCreatePageCommand
    public static Command GetLaunchAddressCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 3.2. GetLaunchAddressDeletePageCommand
    public static Command<int> GetLaunchAddressDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 3.3. GetLaunchAddressDetailsPageCommand
    public static Command<int> GetLaunchAddressDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 3.4. GetLaunchAddressEditPageCommand
    public static Command<int> GetLaunchAddressEditPageCommand(string returnPath)
    {
        return new Command<int>(async (addressID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.AddressEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 3.7. GetLaunchAddressDeletePopupCommand
    public static Command<int> GetLaunchAddressDeletePopupCommand()
    {
        return new Command<int>(async (addressID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<AddressIdentifierMessage>(new AddressIdentifierMessage(new AddressIdentifier { AddressID = addressID }, ViewItemTemplates.Delete));
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

    // 3.10. GetLaunchAddressAdvancedSearchPopupCommand
    public static Command GetLaunchAddressAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 3.11. GetLaunchAddressListBulkActionsPopupCommand
    public static Command GetLaunchAddressListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Address.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 4.1. GetLaunchCustomerCreatePageCommand
    public static Command GetLaunchCustomerCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 4.2. GetLaunchCustomerDeletePageCommand
    public static Command<int> GetLaunchCustomerDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 4.3. GetLaunchCustomerDetailsPageCommand
    public static Command<int> GetLaunchCustomerDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 4.4. GetLaunchCustomerEditPageCommand
    public static Command<int> GetLaunchCustomerEditPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 4.7. GetLaunchCustomerDeletePopupCommand
    public static Command<int> GetLaunchCustomerDeletePopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerIdentifierMessage>(new CustomerIdentifierMessage(new CustomerIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete));
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

    // 4.10. GetLaunchCustomerAdvancedSearchPopupCommand
    public static Command GetLaunchCustomerAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 4.11. GetLaunchCustomerListBulkActionsPopupCommand
    public static Command GetLaunchCustomerListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Customer.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 5.1. GetLaunchCustomerAddressCreatePageCommand
    public static Command GetLaunchCustomerAddressCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 5.2. GetLaunchCustomerAddressDeletePageCommand
    public static Command<int> GetLaunchCustomerAddressDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 5.3. GetLaunchCustomerAddressDetailsPageCommand
    public static Command<int> GetLaunchCustomerAddressDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 5.4. GetLaunchCustomerAddressEditPageCommand
    public static Command<int> GetLaunchCustomerAddressEditPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 5.5. GetLaunchCustomerAddressDashboardPageCommand
    public static Command GetLaunchCustomerAddressDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (customerID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.CustomerAddressDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

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

    // 5.7. GetLaunchCustomerAddressDeletePopupCommand
    public static Command<int> GetLaunchCustomerAddressDeletePopupCommand()
    {
        return new Command<int>(async (customerID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<CustomerAddressIdentifierMessage>(new CustomerAddressIdentifierMessage(new CustomerAddressIdentifier { CustomerID = customerID }, ViewItemTemplates.Delete));
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

    // 5.10. GetLaunchCustomerAddressAdvancedSearchPopupCommand
    public static Command GetLaunchCustomerAddressAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 5.11. GetLaunchCustomerAddressListBulkActionsPopupCommand
    public static Command GetLaunchCustomerAddressListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.CustomerAddress.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 6.1. GetLaunchProductCreatePageCommand
    public static Command GetLaunchProductCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 6.2. GetLaunchProductDeletePageCommand
    public static Command<int> GetLaunchProductDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 6.3. GetLaunchProductDetailsPageCommand
    public static Command<int> GetLaunchProductDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 6.4. GetLaunchProductEditPageCommand
    public static Command<int> GetLaunchProductEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 6.7. GetLaunchProductDeletePopupCommand
    public static Command<int> GetLaunchProductDeletePopupCommand()
    {
        return new Command<int>(async (productID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductIdentifierMessage>(new ProductIdentifierMessage(new ProductIdentifier { ProductID = productID }, ViewItemTemplates.Delete));
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

    // 6.10. GetLaunchProductAdvancedSearchPopupCommand
    public static Command GetLaunchProductAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 6.11. GetLaunchProductListBulkActionsPopupCommand
    public static Command GetLaunchProductListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.Product.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 7.1. GetLaunchProductCategoryCreatePageCommand
    public static Command GetLaunchProductCategoryCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 7.2. GetLaunchProductCategoryDeletePageCommand
    public static Command<int> GetLaunchProductCategoryDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 7.3. GetLaunchProductCategoryDetailsPageCommand
    public static Command<int> GetLaunchProductCategoryDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 7.4. GetLaunchProductCategoryEditPageCommand
    public static Command<int> GetLaunchProductCategoryEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductCategoryEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 7.7. GetLaunchProductCategoryDeletePopupCommand
    public static Command<int> GetLaunchProductCategoryDeletePopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductCategoryIdentifierMessage>(new ProductCategoryIdentifierMessage(new ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, ViewItemTemplates.Delete));
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

    // 7.10. GetLaunchProductCategoryAdvancedSearchPopupCommand
    public static Command GetLaunchProductCategoryAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 7.11. GetLaunchProductCategoryListBulkActionsPopupCommand
    public static Command GetLaunchProductCategoryListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 8.1. GetLaunchProductDescriptionCreatePageCommand
    public static Command GetLaunchProductDescriptionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 8.2. GetLaunchProductDescriptionDeletePageCommand
    public static Command<int> GetLaunchProductDescriptionDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 8.3. GetLaunchProductDescriptionDetailsPageCommand
    public static Command<int> GetLaunchProductDescriptionDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 8.4. GetLaunchProductDescriptionEditPageCommand
    public static Command<int> GetLaunchProductDescriptionEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductDescriptionEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 8.7. GetLaunchProductDescriptionDeletePopupCommand
    public static Command<int> GetLaunchProductDescriptionDeletePopupCommand()
    {
        return new Command<int>(async (productDescriptionID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductDescriptionIdentifierMessage>(new ProductDescriptionIdentifierMessage(new ProductDescriptionIdentifier { ProductDescriptionID = productDescriptionID }, ViewItemTemplates.Delete));
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

    // 8.10. GetLaunchProductDescriptionAdvancedSearchPopupCommand
    public static Command GetLaunchProductDescriptionAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 8.11. GetLaunchProductDescriptionListBulkActionsPopupCommand
    public static Command GetLaunchProductDescriptionListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductDescription.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 9.1. GetLaunchProductModelCreatePageCommand
    public static Command GetLaunchProductModelCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 9.2. GetLaunchProductModelDeletePageCommand
    public static Command<int> GetLaunchProductModelDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 9.3. GetLaunchProductModelDetailsPageCommand
    public static Command<int> GetLaunchProductModelDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 9.4. GetLaunchProductModelEditPageCommand
    public static Command<int> GetLaunchProductModelEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 9.7. GetLaunchProductModelDeletePopupCommand
    public static Command<int> GetLaunchProductModelDeletePopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelIdentifierMessage>(new ProductModelIdentifierMessage(new ProductModelIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete));
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

    // 9.10. GetLaunchProductModelAdvancedSearchPopupCommand
    public static Command GetLaunchProductModelAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 9.11. GetLaunchProductModelListBulkActionsPopupCommand
    public static Command GetLaunchProductModelListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModel.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 10.1. GetLaunchProductModelProductDescriptionCreatePageCommand
    public static Command GetLaunchProductModelProductDescriptionCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 10.2. GetLaunchProductModelProductDescriptionDeletePageCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 10.3. GetLaunchProductModelProductDescriptionDetailsPageCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 10.4. GetLaunchProductModelProductDescriptionEditPageCommand
    public static Command<int> GetLaunchProductModelProductDescriptionEditPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 10.5. GetLaunchProductModelProductDescriptionDashboardPageCommand
    public static Command GetLaunchProductModelProductDescriptionDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (productModelID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.ProductModelProductDescriptionDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

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

    // 10.7. GetLaunchProductModelProductDescriptionDeletePopupCommand
    public static Command<int> GetLaunchProductModelProductDescriptionDeletePopupCommand()
    {
        return new Command<int>(async (productModelID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionIdentifierMessage>(new ProductModelProductDescriptionIdentifierMessage(new ProductModelProductDescriptionIdentifier { ProductModelID = productModelID }, ViewItemTemplates.Delete));
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

    // 10.10. GetLaunchProductModelProductDescriptionAdvancedSearchPopupCommand
    public static Command GetLaunchProductModelProductDescriptionAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 10.11. GetLaunchProductModelProductDescriptionListBulkActionsPopupCommand
    public static Command GetLaunchProductModelProductDescriptionListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductModelProductDescription.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 11.1. GetLaunchSalesOrderDetailCreatePageCommand
    public static Command GetLaunchSalesOrderDetailCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 11.2. GetLaunchSalesOrderDetailDeletePageCommand
    public static Command<int> GetLaunchSalesOrderDetailDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 11.3. GetLaunchSalesOrderDetailDetailsPageCommand
    public static Command<int> GetLaunchSalesOrderDetailDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 11.4. GetLaunchSalesOrderDetailEditPageCommand
    public static Command<int> GetLaunchSalesOrderDetailEditPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit, returnPath));
        });
    }

    // 11.5. GetLaunchSalesOrderDetailDashboardPageCommand
    public static Command GetLaunchSalesOrderDetailDashboardPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderDetailDashboardPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Dashboard, returnPath));
        });
    }

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

    // 11.7. GetLaunchSalesOrderDetailDeletePopupCommand
    public static Command<int> GetLaunchSalesOrderDetailDeletePopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderDetailIdentifierMessage>(new SalesOrderDetailIdentifierMessage(new SalesOrderDetailIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete));
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

    // 11.10. GetLaunchSalesOrderDetailAdvancedSearchPopupCommand
    public static Command GetLaunchSalesOrderDetailAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 11.11. GetLaunchSalesOrderDetailListBulkActionsPopupCommand
    public static Command GetLaunchSalesOrderDetailListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderDetail.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

    // 12.1. GetLaunchSalesOrderHeaderCreatePageCommand
    public static Command GetLaunchSalesOrderHeaderCreatePageCommand(string returnPath)
    {
        return new Command(async () =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderCreatePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(null, ViewItemTemplates.Create, returnPath));
        });
    }

    // 12.2. GetLaunchSalesOrderHeaderDeletePageCommand
    public static Command<int> GetLaunchSalesOrderHeaderDeletePageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDeletePage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete, returnPath));
        });
    }

    // 12.3. GetLaunchSalesOrderHeaderDetailsPageCommand
    public static Command<int> GetLaunchSalesOrderHeaderDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderDetailsPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Details, returnPath));
        });
    }

    // 12.4. GetLaunchSalesOrderHeaderEditPageCommand
    public static Command<int> GetLaunchSalesOrderHeaderEditPageCommand(string returnPath)
    {
        return new Command<int>(async (salesOrderID) =>
        {
            await AppShellService.GoToAbsoluteAsync(AppShellRoutes.SalesOrderHeaderEditPage);
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Edit, returnPath));
        });
    }

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

    // 12.7. GetLaunchSalesOrderHeaderDeletePopupCommand
    public static Command<int> GetLaunchSalesOrderHeaderDeletePopupCommand()
    {
        return new Command<int>(async (salesOrderID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.DeletePopup();
            await Task.Delay(200);
            WeakReferenceMessenger.Default.Send<SalesOrderHeaderIdentifierMessage>(new SalesOrderHeaderIdentifierMessage(new SalesOrderHeaderIdentifier { SalesOrderID = salesOrderID }, ViewItemTemplates.Delete));
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

    // 12.10. GetLaunchSalesOrderHeaderAdvancedSearchPopupCommand
    public static Command GetLaunchSalesOrderHeaderAdvancedSearchPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.AdvancedSearchPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    // 12.11. GetLaunchSalesOrderHeaderListBulkActionsPopupCommand
    public static Command GetLaunchSalesOrderHeaderListBulkActionsPopupCommand()
    {
        return new Command(async () =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.SalesOrderHeader.ListBulkActionsPopup();
            await AppShell.Current.CurrentPage.ShowPopupAsync(popup);
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

