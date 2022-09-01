using CommunityToolkit.Mvvm.ComponentModel;

namespace Framework.MauiX.DataModels
{
    public class NameValuePair : ObservableValidator
    {
        protected string m_Name;
        public string Name
        {
            get => m_Name;
            set => SetProperty(ref m_Name, value);
        }

        protected string m_Value;
        public string Value
        {
            get => m_Value;
            set => SetProperty(ref m_Value, value);
        }
        protected bool m_Selected;
        public bool Selected
        {
            get => m_Selected;
            set => SetProperty(ref m_Selected, value);
        }
    }
}
