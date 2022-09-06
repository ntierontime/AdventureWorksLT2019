using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.DataModels
{
    public class AppStates: ObservableRecipient
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
        }

        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<AdventureWorksLT2019.MauiXApp.DataModels.AppStates, AdventureWorksLT2019.MauiXApp.Messages.GeoLocationChangedMessage>(this, (r, m) => r.CurrentLocation = m.Value);
            WeakReferenceMessenger.Default.Register<AdventureWorksLT2019.MauiXApp.DataModels.AppStates, AdventureWorksLT2019.MauiXApp.Messages.AuthenticatedMessage>(this, (r, m) => r.Authenticated = m.Value);
        }
    }
}
