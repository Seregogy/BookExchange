﻿using System;
using Windows.UI.Xaml.Data;

namespace BookExchange.Helpers;

public class ObjectToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value??"null".ToString();

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}