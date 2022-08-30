namespace Framework.MauiX.DataModels
{
    public class NameValuePair : Framework.MauiX.PropertyChangedNotifier
    {
        protected string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(Name), ref m_Name, value);
            }
        }
        protected string m_Value;
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(Value), ref m_Value, value);
            }
        }
        protected bool m_Selected;
        public bool Selected
        {
            get
            {
                return m_Selected;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(Selected), ref m_Selected, value);
            }
        }
    }
}
