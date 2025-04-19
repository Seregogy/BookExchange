using BookExchange.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;

namespace BookExchange.Helpers.DataProviders;

public class BookDataProvider
{
    public static List<Book> MineBooks { get; set; } = [];
    public static List<Book> BooksToExchange { get; set; } = [];

    public static async Task LoadDataAsync()
    {
        StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(GlobalConst.DEFAULT_PRODUCT_JSON_PATH);
        string json = await FileIO.ReadTextAsync(file);

        var jObj = JObject.Parse(json);

        MineBooks = jObj[nameof(MineBooks)]!.ToObject<List<Book>>()!;
        BooksToExchange = jObj[nameof(BooksToExchange)]!.ToObject<List<Book>>()!;
    }

    public static async void LoadData()
    {
        StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(GlobalConst.DEFAULT_PRODUCT_JSON_PATH);
        string json = await FileIO.ReadTextAsync(file);

        var jObj = JObject.Parse(json);

        MineBooks = jObj[nameof(MineBooks)]!.ToObject<List<Book>>()!;
        BooksToExchange = jObj[nameof(BooksToExchange)]!.ToObject<List<Book>>()!;
    }
}