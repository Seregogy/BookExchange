using BookExchange.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Storage;

namespace BookExchange.Helpers.DataProviders;

public class BookDataProvider : INotifyPropertyChanged
{
    public static List<Book> MineBooks { get; private set; } = [];
    public static List<Book> BooksToExchange { get; private set; } = [];

    private static BookDataProvider? instance;

    public event PropertyChangedEventHandler? PropertyChanged;

    public static BookDataProvider Init()
    {
        instance ??= new BookDataProvider();

        return instance;
    }

    public static async Task LoadDataAsync()
    {
        StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(GlobalConst.DEFAULT_PRODUCT_JSON_PATH);
        string json = await FileIO.ReadTextAsync(file);

        var jObj = JObject.Parse(json);

        MineBooks = jObj[nameof(MineBooks)]!.ToObject<List<Book>>()!;
        BooksToExchange = jObj[nameof(BooksToExchange)]!.ToObject<List<Book>>()!;
    }
}