using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.Models;

using CommunityToolkit.Mvvm.Messaging.Messages;
namespace AdventureWorksLT2019.MauiXApp.Messages;

public sealed class BuildVersionIdentifierMessage: IdentifierMessageBase<BuildVersionIdentifier>
{
    public BuildVersionIdentifierMessage(BuildVersionIdentifier value, ViewItemTemplates itemView, string returnPath = null) : base(value, itemView, returnPath)
    {
    }
}

