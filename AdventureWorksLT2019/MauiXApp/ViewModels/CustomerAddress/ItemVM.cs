using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.CustomerAddress;

public class ItemVM : ItemVMBase<CustomerAddressIdentifier, CustomerAddressDataModel, CustomerAddressService, CustomerAddressItemChangedMessage, CustomerAddressItemRequestMessage>
{
    public ItemVM(CustomerAddressService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<CustomerAddressItemChangedMessage>(new CustomerAddressItemChangedMessage(Item, itemView));
    }
}

