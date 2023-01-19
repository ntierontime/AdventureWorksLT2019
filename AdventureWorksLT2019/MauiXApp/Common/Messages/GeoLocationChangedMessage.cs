using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Common.Messages
{
    public class GeoLocationChangedMessage : ValueChangedMessage<NetTopologySuite.Geometries.Point>
    {
        public GeoLocationChangedMessage(NetTopologySuite.Geometries.Point value) : base(value)
        {
        }
    }
}

