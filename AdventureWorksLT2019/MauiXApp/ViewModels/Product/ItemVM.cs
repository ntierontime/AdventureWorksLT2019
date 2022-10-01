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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Product;

public class ItemVM : ItemVMBase<ProductIdentifier, ProductDataModel, ProductService, ProductItemChangedMessage>
{
    #region Foreign Key SelectLists

    // ForeignKeys.1. ProductCategoryIDList
    private List<NameValuePair<int>> m_ProductCategoryIDList;
    public List<NameValuePair<int>> ProductCategoryIDList
    {
        get => m_ProductCategoryIDList;
        set => SetProperty(ref m_ProductCategoryIDList, value);
    }

    private NameValuePair<int> m_SelectedProductCategoryID;
    public NameValuePair<int> SelectedProductCategoryID
    {
        get => m_SelectedProductCategoryID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedProductCategoryID, value);
                Item.ProductCategoryID = value.Value;
            }
        }
    }

    // ForeignKeys.2. ProductModelIDList
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

    // ForeignKeys.3. ParentIDList
    private List<NameValuePair<int>> m_ParentIDList;
    public List<NameValuePair<int>> ParentIDList
    {
        get => m_ParentIDList;
        set => SetProperty(ref m_ParentIDList, value);
    }

    private NameValuePair<int> m_SelectedParentID;
    public NameValuePair<int> SelectedParentID
    {
        get => m_SelectedParentID;
        set
        {
            if (value != null)
            {
                SetProperty(ref m_SelectedParentID, value);
                Item.ParentID = value.Value;
            }
        }
    }
#endregion Foreign Key SelectLists

    public ICommand LaunchProductCategoryFKItemViewCommand { get; private set; }

    public ICommand LaunchProductModelFKItemViewCommand { get; private set; }
    public ItemVM(ProductService dataService)
        : base(dataService)
    {

        LaunchProductCategoryFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryEditPopupCommand();
        //LaunchProductCategoryFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductCategoryEditPageCommand(AppShellRoutes.ProductListPage);

        LaunchProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelEditPopupCommand();
        //LaunchProductModelFKItemViewCommand = LaunchViewCommandsHelper.GetLaunchProductModelEditPageCommand(AppShellRoutes.ProductListPage);

        WeakReferenceMessenger.Default.Register<ItemVM, ProductIdentifierMessage>(
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

        // ForeignKeys.2. ProductModelIDList
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

        // ForeignKeys.3. ParentIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductCategoryCodeList(new ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ParentIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedParentID = ParentIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedParentID = ParentIDList.FirstOrDefault(t=>t.Value == Item.ParentID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductItemChangedMessage>(new ProductItemChangedMessage(Item, itemView));
    }
}

