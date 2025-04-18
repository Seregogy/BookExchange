using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace BookExchange.Helpers;

public partial class UriToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string imagePath)
            if (Uri.TryCreate(imagePath, UriKind.Relative, out Uri? uri))
                return new BitmapImage(uri);

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
