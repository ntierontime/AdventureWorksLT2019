using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventureWorksLT2019.MauiXApp.Common.Messages
{
    public class GeoLocationChangedMessage : ValueChangedMessage<NetTopologySuite.Geometries.Geometry>
    {
        public GeoLocationChangedMessage(NetTopologySuite.Geometries.Geometry value) : base(value)
        {
        }
    }
}

