using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.MauiX.ViewModels;
using Framework.Models;
using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ErrorLog;

public class ItemVM : ItemVMBase<ErrorLogIdentifier, ErrorLogDataModel, ErrorLogService, ErrorLogItemChangedMessage>
{

    public ItemVM(ErrorLogService dataService)
        : base(dataService)
    {

        WeakReferenceMessenger.Default.Register<ItemVM, ErrorLogIdentifierMessage>(
           this, async (r, m) =>
        {
            if (m.ItemView == ViewItemTemplates.Create)
            {
                Item = _dataService.GetDefault();
            }
            else
            {
                var response = await _dataService.Get(m.Value);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    Item = response.ResponseBody;
                }
            }

        });
    }

    protected override void SendDataChangedMessage(ViewItemTemplates itemView)
    {
        WeakReferenceMessenger.Default.Send<ErrorLogItemChangedMessage>(new ErrorLogItemChangedMessage(Item, itemView));
    }
}

