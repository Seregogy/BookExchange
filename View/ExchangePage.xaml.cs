using Store.Model;
using Store.View.Controls;
using Store.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Store.View;

public sealed partial class ExchangePage : Page, INotifyPropertyChanged
{
    private ObservableCollection<ProductItemCart> products = [];
    private ObservableCollection<CartProductData> productDatas = [];
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private CartViewModel Cart;

    private double totalCost;
    public double TotalCost 
    { 
        get => totalCost; 
        set
        {
            totalCost = value;

            OnPropertyChanged();
        }
    }
    private double TotalSum =>
        Math.Round(productDatas.Select(x => x.product.Cost * x.cart.CountOf(x.product)).Sum());

    private Visibility EmptyCartUiVisible
    {
        get
        {
            if (products.Count == 0)
                return Visibility.Visible;
            
            return Visibility.Collapsed;
        }
    }

    private bool MakeOrderButtonEnabled
    {
        get
        {
            if (products.Count == 0)
                return false;
            
            return true;
        }
    }

    public ExchangePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        Cart = CartViewModel.Init();

        try
        {
            foreach (var product in Cart.Products)
            {
                products.Add(
                    new ProductItemCart(
                        new ProductViewModel(product.Key),
                        (_, product) => Frame.Navigate(typeof(BookPage), product),
                        (_, product) =>
                        {
                            var removableObject = productDatas.Where(x => x.product.Id == product.Product.Id).First();
                            productDatas.Remove(removableObject);

                            productDatas.Add(new CartProductData(Cart, product!.Product));

                            UpdateBindings();
                        },
                        (sender, product) =>
                        {
                            var removableObject = productDatas.Where(x => x.product.Id == product.Product.Id).First();
                            productDatas.Remove(removableObject);
                            products.Remove((sender as ProductItemCart)!);

                            UpdateBindings();
                        }
                    )
                );

                productDatas.Add(new CartProductData(Cart, product.Key));
            }
        }
        catch (Exception ex)
        {
            _ = new ContentDialog()
            {
                Title = ex.Message,
                Content = ex.StackTrace,
                PrimaryButtonText = "ОК",
            }.ShowAsync();
        }
    }

    private void UpdateBindings()
    {
        OnPropertyChanged(nameof(products));
        OnPropertyChanged(nameof(productDatas));
        OnPropertyChanged(nameof(TotalSum));
    }

    private void BackButton_Click(object sender, RoutedEventArgs e) =>
        Frame.GoBack();

    private void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    private void CreateOrder(object sender, RoutedEventArgs e)
    {
        Cart.Products.Clear();
        Frame.GoBack();
    }
}

record CartProductData(CartViewModel cart, Product product)
{
    public string ProductName =>
        product.Name;

    public int CountInCart =>
        cart.CountOf(product);

    public double Cost =>
        product.Cost;

    public double TotalCost =>
        product.Cost * CountInCart;
}