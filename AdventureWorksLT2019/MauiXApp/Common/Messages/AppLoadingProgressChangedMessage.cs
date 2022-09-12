using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Common.Messages
{
    public class AppLoadingProgressChangedMessage : ValueChangedMessage<double>
    {
        public AppLoadingProgressChangedMessage(double value) : base(value)
        {
        }
    }
}
