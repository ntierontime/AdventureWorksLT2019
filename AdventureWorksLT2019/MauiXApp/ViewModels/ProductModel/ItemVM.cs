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

namespace AdventureWorksLT2019.MauiXApp.ViewModels.ProductModel;

public class ItemVM : ItemVMBase<ProductModelIdentifier, ProductModelDataModel, ProductModelService, ProductModelItemChangedMessage>
{

    public ItemVM(ProductModelService dataService)
        : base(dataService)
    {

        WeakReferenceMessenger.Default.Register<ItemVM, ProductModelIdentifierMessage>(
           this, async (r, m) =>
        {
            if (m.ItemView == ViewItemTemplates.Dashboard)
                return;

            ItemView = m.ItemView;
            ReturnPath = m.ReturnPath;

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
        WeakReferenceMessenger.Default.Send<ProductModelItemChangedMessage>(new ProductModelItemChangedMessage(Item, itemView));
    }
}

