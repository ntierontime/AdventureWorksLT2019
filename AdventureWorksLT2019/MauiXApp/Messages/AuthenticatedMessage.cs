using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages
{
    public class AuthenticatedMessage : ValueChangedMessage<bool>
    {
        public AuthenticatedMessage(bool value) : base(value)
        {
        }
    }
}
