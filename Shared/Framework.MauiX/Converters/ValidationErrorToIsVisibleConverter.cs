using System.Globalization;

namespace Framework.MauiX.Converters
{
    public class ValidationErrorToIsVisibleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
            //if (value == null || parameter == null)
            //    return false;
            //if (!(value is Dictionary<string, List<string>>))
            //    return false;
            //var typedValue = (Dictionary<string, List<string>>)value;
            //if (!typedValue.ContainsKey((string)parameter))
            //    return false;
            //var errors = typedValue[(string)parameter];

            //// Get first error if any
            //return errors.Any();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

