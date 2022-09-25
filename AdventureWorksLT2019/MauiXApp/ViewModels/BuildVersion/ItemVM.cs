using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.BuildVersion;

public class ItemVM : ItemVMBase<BuildVersionIdentifier, BuildVersionDataModel, BuildVersionService, BuildVersionItemChangedMessage, BuildVersionItemRequestMessage>
{
    public ItemVM(BuildVersionService dataService)
        : base(dataService)
    {
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<BuildVersionItemChangedMessage>(new BuildVersionItemChangedMessage(Item, itemView));
    }
}

