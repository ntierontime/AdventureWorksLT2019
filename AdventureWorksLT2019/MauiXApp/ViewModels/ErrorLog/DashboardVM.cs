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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ErrorLog;

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

    private ErrorLogDataModel m___Master__;
    public ErrorLogDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    private readonly ErrorLogService _dataService;

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(ErrorLogService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, ErrorLogIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(ErrorLogIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - ErrorLogCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(ErrorLogCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[ErrorLogCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

    }
}

