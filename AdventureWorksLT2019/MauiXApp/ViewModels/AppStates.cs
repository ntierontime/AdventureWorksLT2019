
/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-maccatalyst)'
Before:
using CommunityToolkit.Mvvm.ComponentModel;
After:
using AdventureWorksLT2019;
using AdventureWorksLT2019.MauiXApp;
using AdventureWorksLT2019.MauiXApp.ViewModels;
using AdventureWorksLT2019.MauiXApp.ViewModels;
using AdventureWorksLT2019.MauiXApp.ViewModels.Common;
using CommunityToolkit.Mvvm.ComponentModel;
*/
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class AppStates : ObservableObject
    {
        private bool m_HasAuthentication;
        public bool HasAuthentication
        {
            get => m_HasAuthentication;
            set => SetProperty(ref m_HasAuthentication, value);
        }
        private bool m_Authenticated;
        public bool Authenticated
        {
            get => m_Authenticated;
            set => SetProperty(ref m_Authenticated, value);
        }

        private Microsoft.Spatial.GeographyPoint m_CurrentLocation;
        public Microsoft.Spatial.GeographyPoint CurrentLocation
        {
            get => m_CurrentLocation;
            set => SetProperty(ref m_CurrentLocation, value);
        }

        public AppStates()
        {
            OnActivated();
        }

        protected void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<AdventureWorksLT2019.MauiXApp.ViewModels.AppStates, AdventureWorksLT2019.MauiXApp.Common.Messages.GeoLocationChangedMessage>(this, (r, m) => r.CurrentLocation = m.Value);
            WeakReferenceMessenger.Default.Register<AdventureWorksLT2019.MauiXApp.ViewModels.AppStates, AdventureWorksLT2019.MauiXApp.Common.Messages.AuthenticatedMessage>(this, (r, m) => r.Authenticated = m.Value);
        }
    }
}
