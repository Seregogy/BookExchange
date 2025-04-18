using System;
using Windows.Storage;

namespace Store.Helpers;

public static class GlobalConst
{
    private static string directory = ApplicationData.Current.LocalFolder.Path;
    public static readonly Uri DEFAULT_PRODUCT_JSON_PATH = new Uri("ms-appx:///Resources/products.json");
}