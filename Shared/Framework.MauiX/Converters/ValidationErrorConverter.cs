using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Framework.MauiX.Converters;

public class ValidationErrorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return string.Empty;
        if (!(value is IEnumerable<ValidationResult>))
            return string.Empty;
        var typedValue = (IEnumerable<ValidationResult>)value;
        if (!typedValue.Any(t=>t.MemberNames.Any(t1 => t1 == parameter.ToString())))
            return string.Empty;
        var error = typedValue.First(t => t.MemberNames.Any(t1 => t1 == parameter.ToString()));

        // Get first error if any
        return error.ErrorMessage;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

