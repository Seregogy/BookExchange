using BookExchange.Model;
using BookExchange.Helpers.DataProviders;
using BookExchange.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace BookExchange;

public sealed partial class MainPage : Page, INotifyPropertyChanged
{
    private bool isLoad;
    public bool IsLoad
    {
        get => isLoad;
        set
        {
            isLoad = value;

            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private static UIElement? lastSelected;

    public MainPage()
    {
        InitializeComponent();
        
        InitMainPage();
    }

    private void InitMainPage()
    {
        BookDataProvider.LoadData();
        InitGridViews();

        IsLoad = true;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ConnectedAnimationService
            .GetForCurrentView()
            .GetAnimation(nameof(MainPage))?
            .TryStart(lastSelected ?? MineBooksGrid);
    }

    private void InitGridViews()
    {
        MineBooksGrid.Items.Clear();
        OthersBookGrid.Items.Clear();

        foreach (var book in BookDataProvider.MineBooks)
        {
            MineBooksGrid.Items.Add(new BookCard(book, LaunchBookPage));
            Debug.WriteLine(book);
        }

        Debug.WriteLine("---------------\n");

        foreach (var book in BookDataProvider.BooksToExchange)
        {
            OthersBookGrid.Items.Add(new BookCard(book, LaunchBookPage));
            Debug.WriteLine(book);
        }
    }

    public void LaunchBookPage(object sender, Book? book, UIElement uiElement)
    {
        lastSelected = uiElement;

        var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(nameof(BookPage), uiElement ?? MineBooksGrid);

        anim.Configuration = new DirectConnectedAnimationConfiguration();
        Frame.Navigate(typeof(BookPage), book, new DrillInNavigationTransitionInfo());
    }

    private void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    private Visibility BoolToVisibility(bool value) => 
        value ? Visibility.Visible : Visibility.Collapsed;
    private Visibility ReverseVisibility(bool value) => BoolToVisibility(!value);
}