using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Common.Messages
{
    public class AuthenticatedMessage : ValueChangedMessage<bool>
    {
        public AuthenticatedMessage(bool value) : base(value)
        {
        }
    }
}

