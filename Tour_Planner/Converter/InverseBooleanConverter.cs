using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Tour_Planner.Converter;

public class InverseBooleanConverter : MarkupExtension, IValueConverter {
    private static readonly InverseBooleanConverter Instance = new InverseBooleanConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is bool boolValue) {
            return !boolValue;
        }

        return false;
    }
    
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is bool boolValue) {
            return !boolValue;
        }

        return false;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) {
        return Instance;
    }
}