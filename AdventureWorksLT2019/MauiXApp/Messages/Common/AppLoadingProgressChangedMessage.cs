using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages.Common
{
    public class AppLoadingProgressChangedMessage : ValueChangedMessage<double>
    {
        public AppLoadingProgressChangedMessage(double value) : base(value)
        {
        }
    }
}
