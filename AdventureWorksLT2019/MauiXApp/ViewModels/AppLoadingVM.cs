
/* Unmerged change from project 'AdventureWorksLT2019.MauiXApp (net6.0-ios)'
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
    public class AppLoadingVM : ObservableObject
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
            WeakReferenceMessenger.Default.Register<AppLoadingVM, Common.Messages.AppLoadingProgressChangedMessage>(this, (r, m) =>
            {
                r.Progress = m.Value;
            });
        }
    }
}
