using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class CustomerItemChangedMessage : Framework.MauiX.ComponentModels.ValueChangedMessageExt<DataModels.CustomerDataModel>
{
    public CustomerItemChangedMessage(DataModels.CustomerDataModel value, Framework.Models.ViewItemTemplates itemView) : base(value, itemView)
    {
    }
}
