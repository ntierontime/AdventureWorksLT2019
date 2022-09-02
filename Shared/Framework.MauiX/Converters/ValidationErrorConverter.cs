using System.Globalization;

namespace Framework.MauiX.Converters
{
    public class ValidationErrorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
            //if (value == null || parameter == null)
            //    return string.Empty;
            //if (!(value is Dictionary<string, List<string>>))
            //    return string.Empty;
            //var typedValue = (Dictionary<string, List<string>>)value;
            //if (!typedValue.ContainsKey((string)parameter))
            //    return string.Empty;
            //var errors = typedValue[(string)parameter];

            //// Get first error if any
            //return errors.FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

