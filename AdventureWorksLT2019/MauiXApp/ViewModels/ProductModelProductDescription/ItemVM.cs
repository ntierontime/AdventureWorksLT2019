using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductModelProductDescription;

public class ItemVM : ItemVMBase<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel, ProductModelProductDescriptionService, ProductModelProductDescriptionItemChangedMessage, ProductModelProductDescriptionItemRequestMessage>
{
    public ItemVM(ProductModelProductDescriptionService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductModelProductDescriptionItemChangedMessage>(new ProductModelProductDescriptionItemChangedMessage(Item, itemView));
    }
}

