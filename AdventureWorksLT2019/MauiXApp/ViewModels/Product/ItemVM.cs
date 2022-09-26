using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Product;

public class ItemVM : ItemVMBase<ProductIdentifier, ProductDataModel, ProductService, ProductItemChangedMessage, ProductItemRequestMessage>
{
    private List<Framework.Models.NameValuePair<int>> m_ProductCategoryList;
    public List<Framework.Models.NameValuePair<int>> ProductCategoryList
    {
        get => m_ProductCategoryList;
        set => SetProperty(ref m_ProductCategoryList, value);
    }

    private Framework.Models.NameValuePair<int> m_SelectedProductCategory;
    public Framework.Models.NameValuePair<int> SelectedProductCategory
    {
        get => m_SelectedProductCategory;
        set
        {
            SetProperty(ref m_SelectedProductCategory, value);
            Item.ProductCategoryID = value.Value;
        }
    }

    public ItemVM(ProductService dataService)
        : base(dataService)
    {
    }

    protected override async Task LoadCodeListsIfAny(Framework.Models.ViewItemTemplates itemView)
    {
        // 1. ProductCategoryList
        {
            var productCategoryService = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.Services.ProductCategoryService>();
            var response = await productCategoryService.GetCodeList(new AdventureWorksLT2019.MauiXApp.DataModels.ProductCategoryAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                ProductCategoryList = new List<Framework.Models.NameValuePair<int>>(response.ResponseBody);
                if (itemView == Framework.Models.ViewItemTemplates.Create)
                {
                    SelectedProductCategory = ProductCategoryList.First();
                }
                else if (itemView == Framework.Models.ViewItemTemplates.Edit)
                {
                    SelectedProductCategory = ProductCategoryList.First(t=>t.Value == Item.ProductCategoryID);
                }
            }
        }
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductItemChangedMessage>(new ProductItemChangedMessage(Item, itemView));
    }
}

