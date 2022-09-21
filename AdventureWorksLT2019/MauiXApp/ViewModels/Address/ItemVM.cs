using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Address;

public class ItemVM : ItemVMBase<AddressIdentifier, AddressDataModel, AddressService, AddressItemChangedMessage, AddressItemRequestMessage>
{
    public ItemVM(AddressService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<AddressItemChangedMessage>(new AddressItemChangedMessage(Item, itemView));
    }
}

