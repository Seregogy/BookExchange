using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Store.Helpers;

public class ImageDecoder
{
    public string PathToImage { get; set; }
    
    public ImageDecoder() : this(string.Empty) { }

    public ImageDecoder(string pathToFile)
    {
        PathToImage = pathToFile;
    }
   
    public async Task<BitmapImage> DecodeImage() =>
        await DecodeImage(PathToImage);

    public async Task<BitmapImage> DecodeImage(string path)
    {
        return null;
    }
}