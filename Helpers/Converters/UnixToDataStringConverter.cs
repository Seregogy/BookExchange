using System;
using Windows.UI.Xaml.Data;

namespace Store.Helpers.Converters;

public class UnixToDataStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is long unixTimeStamp)
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).ToString("dd.MM.yy HH:mm");

        return "null";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}