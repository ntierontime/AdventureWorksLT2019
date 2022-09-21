using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductCategory;

public class ItemVM : ItemVMBase<ProductCategoryIdentifier, ProductCategoryDataModel, ProductCategoryService, ProductCategoryItemChangedMessage, ProductCategoryItemRequestMessage>
{
    public ItemVM(ProductCategoryService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductCategoryItemChangedMessage>(new ProductCategoryItemChangedMessage(Item, itemView));
    }
}

