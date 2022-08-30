namespace Framework.Mvvm.Models
{
    public class NameValuePair : Framework.Mvvm.Models.PropertyChangedNotifier
    {
        protected string? _Name;
        public string? Name
        {
            get
            {
                return _Name;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(Name), ref _Name, value);
            }
        }
        protected string? _Value;
        public string? Value
        {
            get
            {
                return _Value;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(Value), ref _Value, value);
            }
        }
        protected bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                //ValidateProperty(value);
                Set(nameof(Selected), ref _Selected, value);
            }
        }
    }
}
