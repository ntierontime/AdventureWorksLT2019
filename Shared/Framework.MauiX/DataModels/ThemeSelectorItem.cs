namespace Framework.MauiX.DataModels
{
    public class ThemeSelectorItem : Framework.MauiX.PropertyChangedNotifier
    {
        private Framework.Common.Theme m_Theme;
        public Framework.Common.Theme Theme
        {
            get { return m_Theme; }
            set
            {
                Set(nameof(Theme), ref m_Theme, value);
            }
        }

        private bool m_IsCurrent;
        public bool IsCurrent
        {
            get { return m_IsCurrent; }
            set
            {
                Set(nameof(IsCurrent), ref m_IsCurrent, value);
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

        private ResourceDictionary m_ResourceDictionary;
        public ResourceDictionary ResourceDictionary
        {
            get { return m_ResourceDictionary; }
            set
            {
                Set(nameof(ResourceDictionary), ref m_ResourceDictionary, value);
            }
        }
    }
}
