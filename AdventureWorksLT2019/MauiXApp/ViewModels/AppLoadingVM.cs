using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class AppLoadingVM: ObservableObject
    {
        private double m_Progress = 0;
        public double Progress
        {
            get => m_Progress;
            set => SetProperty(ref m_Progress, value);
        }

        private double m_Scale = 4;
        public double Scale
        {
            get => m_Scale;
            set => SetProperty(ref m_Scale, value);
        }

        public AppLoadingVM()
        {
            OnActivated();
        }

        protected void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<AdventureWorksLT2019.MauiXApp.ViewModels.AppLoadingVM, AdventureWorksLT2019.MauiXApp.Messages.AppLoadingProgressChangedMessage>(this, (r, m) =>
            {
                r.Progress = m.Value;
            });
        }
    }
}
