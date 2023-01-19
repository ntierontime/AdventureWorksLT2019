using AdventureWorksLT2019.MauiXApp.Common.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Common.Services;

public class GeoLocationService
{
    public async Task GetCurrentLocation()
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                    return;
            }
            Location location = null;
            // for (var i = 0; i < 10; i++) // usually, we don't need loop 10 times
            {
                // if (location == null)
                {
                    location = await Geolocation.Default.GetLastKnownLocationAsync();
                }
                if (location == null)
                {
                    location = await Geolocation.Default.GetLocationAsync();
                }

                //if (location == null)
                //{
                //    Thread.Sleep(1000);
                //}
                //else
                //{
                //    break;
                //}
            }
            if (location != null)
            {
                //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                WeakReferenceMessenger.Default.Send<GeoLocationChangedMessage>(new GeoLocationChangedMessage(new NetTopologySuite.Geometries.Point(location.Latitude, location.Longitude)));
            }
        }
        //catch (FeatureNotSupportedException fnsEx)
        //{
        //    // Handle not supported on device exception
        //}
        //catch (FeatureNotEnabledException fneEx)
        //{
        //    // Handle not enabled on device exception
        //}
        //catch (PermissionException pEx)
        //{
        //    // Handle permission exception
        //}
        catch //(Exception ex)
        {
            // Unable to get location
        }
    }
}

