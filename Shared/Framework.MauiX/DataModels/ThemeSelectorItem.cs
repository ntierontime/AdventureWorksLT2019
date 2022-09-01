namespace Framework.MauiX.DataModels
{
    public class ThemeSelectorItem : Framework.MauiX.PropertyChangedNotifier
    {
        private AppTheme m_Theme;
        public AppTheme Theme
        {
            get { return m_Theme; }
            set
            {
                Set(nameof(Theme), ref m_Theme, value);
            }
        }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            set
            {
                Set(nameof(Text), ref m_Text, value);
            }
        }
    }
}
