using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory;

public class ItemVM : ItemVMBase<ProductCategoryIdentifier, ProductCategoryDataModel, ProductCategoryService, ProductCategoryItemChangedMessage, ProductCategoryItemRequestMessage>
{

    // ForeignKeys.1. ParentProductCategoryIDList
    private List<NameValuePair<int>> m_ParentProductCategoryIDList;
    public List<NameValuePair<int>> ParentProductCategoryIDList
    {
        get => m_ParentProductCategoryIDList;
        set => SetProperty(ref m_ParentProductCategoryIDList, value);
    }

    private NameValuePair<int> m_SelectedParentProductCategoryID;
    public NameValuePair<int> SelectedParentProductCategoryID
    {
        get => m_SelectedParentProductCategoryID;
        set
        {
            SetProperty(ref m_SelectedParentProductCategoryID, value);
            Item.ParentProductCategoryID = value.Value;
        }
    }
    public ItemVM(ProductCategoryService dataService)
        : base(dataService)
    {
    }

    protected override async Task LoadCodeListsIfAny(ViewItemTemplates itemView)
    {

        // ForeignKeys.1. ParentProductCategoryIDList
        {
            var codeListsApiService = ServiceHelper.GetService<CodeListsApiService>();
            var response = await codeListsApiService.GetProductCategoryCodeList(new ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ParentProductCategoryIDList = new List<NameValuePair<int>>(response.ResponseBody);
                if (itemView == ViewItemTemplates.Create)
                {
                    SelectedParentProductCategoryID = ParentProductCategoryIDList.FirstOrDefault();
                }
                else if (itemView == ViewItemTemplates.Edit)
                {
                    SelectedParentProductCategoryID = ParentProductCategoryIDList.FirstOrDefault(t=>t.Value == Item.ParentProductCategoryID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductCategoryItemChangedMessage>(new ProductCategoryItemChangedMessage(Item, itemView));
    }
}

