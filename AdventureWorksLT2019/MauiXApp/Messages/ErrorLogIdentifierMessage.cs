using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.Models;

using CommunityToolkit.Mvvm.Messaging.Messages;
namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class ErrorLogIdentifierMessage: IdentifierMessageBase<ErrorLogIdentifier>
{
    public ErrorLogIdentifierMessage(ErrorLogIdentifier value, ViewItemTemplates itemView, string returnPath = null) : base(value, itemView, returnPath)
    {
    }
}

