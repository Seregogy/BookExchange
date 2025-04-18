using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Store.Helpers.Converters;

public class BoolToImageStretch : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool toggle)
            return toggle ? Stretch.UniformToFill : Stretch.Uniform;

        return default;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}