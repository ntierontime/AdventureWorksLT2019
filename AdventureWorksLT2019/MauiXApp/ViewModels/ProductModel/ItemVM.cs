using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel;

public class ItemVM : ItemVMBase<ProductModelIdentifier, ProductModelDataModel, ProductModelService, ProductModelItemChangedMessage, ProductModelItemRequestMessage>
{
    public ItemVM(ProductModelService dataService)
        : base(dataService)
    {
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductModelItemChangedMessage>(new ProductModelItemChangedMessage(Item, itemView));
    }
}

