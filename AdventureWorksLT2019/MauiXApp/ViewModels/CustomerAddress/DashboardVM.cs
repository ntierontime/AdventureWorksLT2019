using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress;

public class DashboardVM : ObservableObject
{
    private string m_ReturnPath;
    /// <summary>
    /// where to go, if we have Close button
    /// </summary>
    public string ReturnPath
    {
        get => m_ReturnPath;
        set => SetProperty(ref m_ReturnPath, value);
    }

    private CustomerAddressDataModel m___Master__;
    public CustomerAddressDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    private readonly CustomerAddressService _dataService;

    public ICommand LaunchMaster_CustomerFKItemViewCommand { get; private set; }

    public ICommand LaunchMaster_AddressFKItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(CustomerAddressService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, CustomerAddressIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        LaunchMaster_CustomerFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPopupCommand();
        //LaunchMaster_CustomerFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPageCommand(AppShellRoutes.CustomerAddressListPage);

        LaunchMaster_AddressFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchAddressDetailsPopupCommand();
        //LaunchMaster_AddressFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchAddressDetailsPageCommand(AppShellRoutes.CustomerAddressListPage);

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(CustomerAddressIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - CustomerAddressCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(CustomerAddressCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[CustomerAddressCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

    }
}

