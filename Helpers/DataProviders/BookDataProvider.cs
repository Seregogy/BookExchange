using Newtonsoft.Json;
using Store.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace Store.Helpers.DataProviders;

public class BookDataProvider : INotifyPropertyChanged
{
    public static List<Product> Data { get; private set; } = [];

    private static BookDataProvider? instance;

    public event PropertyChangedEventHandler? PropertyChanged;

    public static BookDataProvider Init()
    {
        instance ??= new BookDataProvider();

        return instance;
    }

    public async Task LoadDataAsync()
    {
        try
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(GlobalConst.DEFAULT_PRODUCT_JSON_PATH);
            string json = await FileIO.ReadTextAsync(file);

            Data = [.. JsonConvert.DeserializeObject<Product[]>(json)!];

            Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
    }
}