using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class CustomerAddressItemChangedMessage: ValueChangedMessageExt<CustomerAddressDataModel>
{
    public CustomerAddressItemChangedMessage(CustomerAddressDataModel value, ViewItemTemplates itemView) : base(value, itemView)
    {
    }
}

