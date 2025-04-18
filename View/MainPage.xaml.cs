using BookExchange.Model;
using BookExchange.Helpers.DataProviders;
using BookExchange.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BookExchange;

public sealed partial class MainPage : Page, INotifyPropertyChanged
{
    private ObservableCollection<Book> products = [];

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

    public MainPage()
    {
        InitializeComponent();
        
        InitMainPage();
    }

    private void InitMainPage()
    {
        _ = Task.Run(async () =>
        {
            await BookDataProvider.LoadDataAsync();

            _ = Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                foreach (var product in BookDataProvider.MineBooks)
                    MineBooksGrid.Items.Add(new BookCard(product, LaunchBookPage));

                foreach (var product in BookDataProvider.BooksToExchange)
                    OthersBookGrid.Items.Add(new BookCard(product, LaunchBookPage));

                IsLoad = true;
            });
        });
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ConnectedAnimationService
            .GetForCurrentView()
            .GetAnimation("DirectConnectedAnimation")?
            .TryStart(MineBooksGridShimmer);
    }

    public void LaunchBookPage(object sender, Book? book, UIElement uiElement)
    {
        var connectedAnimation = ConnectedAnimationService
                                    .GetForCurrentView()
                                    .PrepareToAnimate("DirectConnectedAnimation", uiElement ?? MineBooksGrid);
        
        connectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
        Frame.Navigate(typeof(BookPage), book, new DrillInNavigationTransitionInfo());
    }

    private void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    private Visibility BoolToVisibility(bool value) => 
        value ? Visibility.Visible : Visibility.Collapsed;
    private Visibility ReverseVisibility(bool value) => BoolToVisibility(!value);
}