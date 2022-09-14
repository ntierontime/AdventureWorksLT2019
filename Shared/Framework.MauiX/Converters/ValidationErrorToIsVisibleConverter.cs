using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Framework.MauiX.Converters;

public class ValidationErrorToIsVisibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;
        if (!(value is IEnumerable<ValidationResult>))
            return false;
        var typedValue = (IEnumerable<ValidationResult>)value;
        return typedValue.Any(t => t.MemberNames.Any(t1 => t1 == parameter.ToString()));
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

