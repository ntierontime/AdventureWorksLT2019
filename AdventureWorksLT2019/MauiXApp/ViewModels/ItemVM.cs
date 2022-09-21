using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

public class ItemVM : Framework.MauiX.ViewModels.ItemVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.Services.CustomerService, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemChangedMessage, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemRequestMessage>
{
    public ItemVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(Framework.Models.ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.CustomerItemChangedMessage>(new AdventureWorksLT2019.MauiXApp.Messages.CustomerItemChangedMessage(Item, itemView));
    }
}
