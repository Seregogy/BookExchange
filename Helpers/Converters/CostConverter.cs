using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Store.Helpers;

partial class CostConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double cost)
        {
            RegionInfo myRI1 = new RegionInfo("RU");
            return $"{cost}{myRI1.CurrencySymbol}";
        }

        return "free";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
