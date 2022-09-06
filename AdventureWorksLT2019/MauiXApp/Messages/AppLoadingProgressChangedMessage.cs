using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages
{
    public class AppLoadingProgressChangedMessage : ValueChangedMessage<double>
    {
        public AppLoadingProgressChangedMessage(double value) : base(value)
        {
        }
    }
}
