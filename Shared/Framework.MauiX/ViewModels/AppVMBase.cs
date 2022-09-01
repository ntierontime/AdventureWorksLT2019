using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System.Xml.Linq;

namespace Framework.MauiX.ViewModels
{
    public class AppVMBase : ObservableObject
    {
        public bool HasAuthentication { get; set; }

        protected bool m_ShellNavBarIsVisible;
        public bool ShellNavBarIsVisible
        {
            get => m_ShellNavBarIsVisible;
            set => SetProperty(ref m_ShellNavBarIsVisible, value);
        }

        private Microsoft.Spatial.GeographyPoint m_CurrentLocation;
        public Microsoft.Spatial.GeographyPoint CurrentLocation
        {
            get => m_CurrentLocation;
            set => SetProperty(ref m_CurrentLocation, value);
        }

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
                for (var i = 0; i < 10; i++)
                {
                    if (CurrentLocation == null)
                    {
                        location = await Geolocation.Default.GetLastKnownLocationAsync();
                    }
                    else
                    {
                        location = await Geolocation.Default.GetLocationAsync();
                    }

                    if (location == null)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        break;
                    }
                }
                if (location != null)
                {
                    CurrentLocation = Microsoft.Spatial.GeographyPoint.Create(location.Latitude, location.Longitude);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
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
}
