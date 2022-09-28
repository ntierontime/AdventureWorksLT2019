using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

public class ItemVM : ItemVMBase<CustomerIdentifier, CustomerDataModel, CustomerService, CustomerItemChangedMessage, CustomerItemRequestMessage>
{

    public ItemVM(CustomerService dataService)
        : base(dataService)
    {
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<CustomerItemChangedMessage>(new CustomerItemChangedMessage(Item, itemView));
    }
}

