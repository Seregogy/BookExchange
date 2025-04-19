using BookExchange.Helpers.DataProviders;
using BookExchange.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BookExchange.View;

public sealed partial class ExchangePage : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private Book? leftBook;
    private Book? rightBook;

    private Visibility RightBookVisibility
    {
        get => RightBook == null ? Visibility.Collapsed : Visibility.Visible;
    }

    private Visibility LeftBookVisibility
    {
        get => LeftBook == null ? Visibility.Collapsed : Visibility.Visible;
    }

    private bool ExchangeButtonEnabled
    {
        get => leftBook != null && rightBook != null;
    }

    public Book? LeftBook 
    {
        get => leftBook;
        set 
        {
            leftBook = value;

            OnPropertyChanged();
        }
    }
    public Book? RightBook 
    { 
        get => rightBook;
        set
        {
            rightBook = value;

            OnPropertyChanged();
        }
    }

    public ExchangePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ConnectedAnimationService
            .GetForCurrentView()
            .GetAnimation(nameof(ExchangePage))?
            .TryStart(MineBook);

        if (e.Parameter is ExchangePageLaunchData data)
        {
            LeftBook = data.LeftBook;
            RightBook = data.RightBook;
            
            UpdateBinds();
        }
    }


    private void BackButtonClick(object sender, RoutedEventArgs e)
    {
        var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(nameof(BookPage), MainGrid);

        anim.Configuration = new DirectConnectedAnimationConfiguration();
        Frame.GoBack(new SlideNavigationTransitionInfo());
    }

    private void RightBookSelected(object sender, ItemClickEventArgs e)
    {
        RightBook = e.ClickedItem as Book;

        UpdateBinds();
    }

    private void ExchangeButtonClick(object sender, RoutedEventArgs e)
    {
        if (LeftBook == null || RightBook == null) return;

        BookDataProvider.MineBooks.Remove(LeftBook);
        BookDataProvider.BooksToExchange.Remove(RightBook);

        BookDataProvider.MineBooks.Add(RightBook);
        BookDataProvider.BooksToExchange.Add(LeftBook);

        var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(nameof(MainPage), MainGrid);
        Frame.Navigate(typeof(MainPage), new DrillInNavigationTransitionInfo());
    }

    private Visibility RevereseVisibility(Visibility visibility) => 
        visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

    private void UpdateBinds()
    {
        OnPropertyChanged(nameof(LeftBook));
        OnPropertyChanged(nameof(RightBook));

        OnPropertyChanged(nameof(LeftBookVisibility));
        OnPropertyChanged(nameof(RightBookVisibility));
        OnPropertyChanged(nameof(ExchangeButtonEnabled));

        OnPropertyChanged(nameof(RevereseVisibility));
    }

    private void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
}

public record class ExchangePageLaunchData(Book LeftBook, Book RightBook);