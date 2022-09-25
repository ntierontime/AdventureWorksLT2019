using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductDescription;

public class ItemVM : ItemVMBase<ProductDescriptionIdentifier, ProductDescriptionDataModel, ProductDescriptionService, ProductDescriptionItemChangedMessage, ProductDescriptionItemRequestMessage>
{
    public ItemVM(ProductDescriptionService dataService)
        : base(dataService)
    {
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ProductDescriptionItemChangedMessage>(new ProductDescriptionItemChangedMessage(Item, itemView));
    }
}

