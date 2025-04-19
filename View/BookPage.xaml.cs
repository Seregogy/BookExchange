using CommunityToolkit.Labs.WinUI.MarkdownTextBlock;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookExchange.Model;

namespace BookExchange.View;

public sealed partial class BookPage : Page, INotifyPropertyChanged
{
    private Book? book;
    public Book? Book 
    { 
        get => book; 
        set
        {
            book = value;

            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public BookPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ConnectedAnimationService
            .GetForCurrentView()
            .GetAnimation(nameof(BookPage))?
            .TryStart(ImageBorder);

        if (e.Parameter is Book book)
        {
            Book = book;
            MarkdownTextBlock.Config = new MarkdownConfig();
        }
    }

    private void BackButtonClick(object sender, RoutedEventArgs e)
    {
        var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(nameof(MainPage), ImageBorder);

        anim.Configuration = new DirectConnectedAnimationConfiguration();
        Frame.GoBack(new SlideNavigationTransitionInfo());
    }

    private void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    private void LaunchExchangePage(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(nameof(ExchangePage), ImageBorder);
        anim.Configuration = new DirectConnectedAnimationConfiguration();

        Frame.Navigate(typeof(ExchangePage), new ExchangePageLaunchData(Book!, null), new DrillInNavigationTransitionInfo());
    }
}
