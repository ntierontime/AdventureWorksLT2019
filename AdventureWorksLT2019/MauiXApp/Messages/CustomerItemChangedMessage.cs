using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class CustomerItemChangedMessage : Framework.MauiX.ComponentModels.ValueChangedMessageExt<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>
{
    public CustomerItemChangedMessage(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel value, Framework.Models.ViewItemTemplates itemView) : base(value, itemView)
    {
    }
}
