namespace Framework.MauiX.ViewModels
{
    public class ProgressBarVM: Framework.MauiX.PropertyChangedNotifier
    {
        private double m_Progress = 0;
        public double Progress
        {
            get { return m_Progress; }
            set
            {
                Set(nameof(Progress), ref m_Progress, value);
            }
        }

        private double m_Scale;
        public double Scale
        {
            get { return m_Scale; }
            set
            {
                Set(nameof(Scale), ref m_Scale, value);
            }
        }

        public void Initialization(double scale)
        {
            Scale = scale;
        }

        public void Go(double progress)
        {
            Progress = progress;
        }
        public void Forward()
        {
            Progress += 0.1;
        }
        public void Backward()
        {
            Progress -= 0.1;
        }
    }
}

