using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;

public class ItemVM : ItemVMBase<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel, ProductModelProductDescriptionService, ProductModelProductDescriptionItemChangedMessage>
{
    #region Foreign Key SelectLists

    // ForeignKeys.1. ProductModelIDList
    private List<NameValuePair<int>> m_ProductModelIDList;
    public List<NameValuePair<int>> ProductModelIDList
    {
        get => m_ProductModelIDList;
        set => SetProperty(ref m_ProductModelIDList, value);
    }

    private NameValuePair<int> m_SelectedProductModelID;
    public NameValuePair<int> SelectedProductModelID
    {
        get => m_SelectedProductModelID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedProductModelID, value);
                Item.ProductModelID = value.Value;
            }
        }
    }

    // ForeignKeys.2. ProductDescriptionIDList
    private List<NameValuePair<int>> m_ProductDescriptionIDList;
    public List<NameValuePair<int>> ProductDescriptionIDList
    {
        get => m_ProductDescriptionIDList;
        set => SetProperty(ref m_ProductDescriptionIDList, value);
    }

    private NameValuePair<int> m_SelectedProductDescriptionID;
    public NameValuePair<int> SelectedProductDescriptionID
    {
        get => m_SelectedProductDescriptionID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedProductDescriptionID, value);
                Item.ProductDescriptionID = value.Value;
            }
        }
    }
#endregion Foreign Key SelectLists

    public ICommand LaunchProductModelFKItemViewCommand { get; private set; }

    public ICommand LaunchProductDescriptionFKItemViewCommand { get; private set; }
    public ItemVM(ProductModelProductDescriptionService dataService)
        : base(dataService)
    {

        LaunchProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelEditPopupCommand();
        //LaunchProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelEditPageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);

        LaunchProductDescriptionFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductDescriptionEditPopupCommand();
        //LaunchProductDescriptionFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductDescriptionEditPageCommand(AppShellRoutes.ProductModelProductDescriptionListPage);

        WeakReferenceMessenger.Default.Register<ItemVM, ProductModelProductDescriptionIdentifierMessage>(
           this, async (r, m) =>
        {
            if (m.ItemView == ViewItemTemplates.Create)
            {
                Item = _dataService.GetDefault();
            }
            else
            {
                var response = await _dataService.Get(m.Value);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    Item = response.ResponseBody;
                }
            }
            if (m.ItemView == ViewItemTemplates.Create || m.ItemView == ViewItemTemplates.Edit)
            {
                await LoadCodeListsIfAny(m.ItemView);
            }
        });
    }

    protected override async Task LoadCodeListsIfAny(ViewItemTemplates itemView)
    {

        // ForeignKeys.1. ProductModelIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductModelCodeList(new ProductModelAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductModelIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedProductModelID = ProductModelIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedProductModelID = ProductModelIDList.FirstOrDefault(t=>t.Value == Item.ProductModelID);
                }
            }
        }

        // ForeignKeys.2. ProductDescriptionIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductDescriptionCodeList(new ProductDescriptionAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductDescriptionIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedProductDescriptionID = ProductDescriptionIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedProductDescriptionID = ProductDescriptionIDList.FirstOrDefault(t=>t.Value == Item.ProductDescriptionID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionItemChangedMessage>(new ProductModelProductDescriptionItemChangedMessage(Item, itemView));
    }
}

