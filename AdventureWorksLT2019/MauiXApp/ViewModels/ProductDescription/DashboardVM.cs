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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription;

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

    private ProductDescriptionDataModel m___Master__;
    public ProductDescriptionDataModel __Master__
    {
        get => m___Master__;
        set => SetProperty(ref m___Master__, value);
    }

    // 4. ListTable = 4,
    private ObservableCollection<ProductModelProductDescriptionDataModel> m_ProductModelProductDescriptions_Via_ProductDescriptionID = new();
    public ObservableCollection<ProductModelProductDescriptionDataModel> ProductModelProductDescriptions_Via_ProductDescriptionID
    {
        get => m_ProductModelProductDescriptions_Via_ProductDescriptionID;
        set => SetProperty(ref m_ProductModelProductDescriptions_Via_ProductDescriptionID, value);
    }

    private readonly ProductDescriptionService _dataService;

    // 4. ListTable = 4,
    public ICommand LaunchList_ProductModelProductDescriptionItemViewCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public DashboardVM(ProductDescriptionService dataService)
    {
        _dataService = dataService;

        WeakReferenceMessenger.Default.Register<DashboardVM, ProductDescriptionIdentifierMessage>(
            this, async (r, m) =>
            {
                if (m.ItemView != ViewItemTemplates.Dashboard)
                    return;
                ReturnPath = m.ReturnPath;
                await LoadData(m.Value);
            });

    // 4. ListTable = 4,

        CloseCommand = AppShellService.ShellGotoAbsoluteCommand;

    }

    public async Task LoadData(ProductDescriptionIdentifier identifier)
    {
        var response = await _dataService.GetCompositeModel(identifier);

        // 1. MasterData - ProductDescriptionCompositeModel
        if (response == null || response.Responses == null ||
            !response.Responses.ContainsKey(ProductDescriptionCompositeModel.__DataOptions__.__Master__))
        {
            //TODO: __Master__ Failed
            return;
        }

        var masterResponse = response.Responses[ProductDescriptionCompositeModel.__DataOptions__.__Master__];
        if(masterResponse.Status != System.Net.HttpStatusCode.OK)
        {
            //TODO: __Master__ Failed
            return;
        }

        __Master__ = response.__Master__;

        // 4. ListTable = 4,

        if(response.Responses.ContainsKey(ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID) &&
            response.Responses[ProductDescriptionCompositeModel.__DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID].Status == System.Net.HttpStatusCode.OK)
        {
            ProductModelProductDescriptions_Via_ProductDescriptionID = new ObservableCollection<ProductModelProductDescriptionDataModel>(response.ProductModelProductDescriptions_Via_ProductDescriptionID);
        }

    }
}

