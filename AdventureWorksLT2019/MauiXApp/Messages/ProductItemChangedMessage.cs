using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class ProductItemChangedMessage: ValueChangedMessageExt<ProductDataModel>
{
    public ProductItemChangedMessage(ProductDataModel value, ViewItemTemplates itemView) : base(value, itemView)
    {
    }
}

