using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderHeader;

public class ItemVM : ItemVMBase<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel, SalesOrderHeaderService, SalesOrderHeaderItemChangedMessage, SalesOrderHeaderItemRequestMessage>
{
    public ItemVM(SalesOrderHeaderService dataService)
        : base(dataService)
    {
    }

    public override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<SalesOrderHeaderItemChangedMessage>(new SalesOrderHeaderItemChangedMessage(Item, itemView));
    }
}

