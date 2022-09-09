using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Messages.Common
{
    public class GeoLocationChangedMessage : ValueChangedMessage<Microsoft.Spatial.GeographyPoint>
    {
        public GeoLocationChangedMessage(Microsoft.Spatial.GeographyPoint value) : base(value)
        {
        }
    }
}
