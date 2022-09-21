using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class CustomerItemChangedMessage: ValueChangedMessageExt<CustomerDataModel>
{
    public CustomerItemChangedMessage(CustomerDataModel value, ViewItemTemplates itemView) : base(value, itemView)
    {
    }
}

