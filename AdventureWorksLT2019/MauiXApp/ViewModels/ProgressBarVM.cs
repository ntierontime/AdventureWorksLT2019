using CommunityToolkit.Mvvm.ComponentModel;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{ 
    public class ProgressBarVM: ObservableObject
    {
        private double m_Progress = 0;
        public double Progress
        {
            get => m_Progress;
            set => SetProperty(ref m_Progress, value);
        }

        private double m_Scale;
        public double Scale
        {
            get => m_Scale;
            set => SetProperty(ref m_Scale, value);
        }

        public void Initialization(double scale)
        {
            Scale = scale;
        }

        public void Go(double progress)
        {
            Progress = progress;
        }
        public void Forward(double progress = 0.1)
        {
            Progress += progress;
        }
        public void Backward(double progress = 0.1)
        {
            Progress -= progress;
        }
    }
}

