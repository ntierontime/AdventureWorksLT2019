using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.SalesOrderDetail;

public class ItemVM : ItemVMBase<SalesOrderDetailIdentifier, SalesOrderDetailDataModel, SalesOrderDetailService, SalesOrderDetailItemChangedMessage, SalesOrderDetailItemRequestMessage>
{
    public ItemVM(SalesOrderDetailService dataService)
        : base(dataService)
    {
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<SalesOrderDetailItemChangedMessage>(new SalesOrderDetailItemChangedMessage(Item, itemView));
    }
}

