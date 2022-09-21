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
    public ItemVM(ProductService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductItemChangedMessage>(new ProductItemChangedMessage(Item, itemView));
    }
}

