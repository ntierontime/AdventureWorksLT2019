using AdventureWorksLT2019.MauiXApp.DataModels;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class ProductCategoryIdentifierMessage: Framework.MauiX.ComponentModels.IdentifierMessageBase<AdventureWorksLT2019.MauiXApp.DataModels.ProductCategoryIdentifier>
{
    public ProductCategoryIdentifierMessage(AdventureWorksLT2019.MauiXApp.DataModels.ProductCategoryIdentifier value, Framework.Models.ViewItemTemplates itemView, string returnPath = null) : base(value, itemView, returnPath)
    {
    }
}
