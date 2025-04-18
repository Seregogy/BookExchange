using Store.Model;
using Store.ViewModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Store.View.Controls;

public sealed partial class AddToCartButton : UserControl, INotifyPropertyChanged
{
    private ProductViewModel? product;
    private CartViewModel? cart;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler? ButtonClicked;

    private Visibility ButtonVisibility
    {
        get => IsButtonVisible();
    }

    private Visibility AppenderVisibility
    {
        get => IsButtonVisible() == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }

    private int ProductInCartCount
    {
        get
        {
            if (cart!.Products.ContainsKey(Product!.Product))
                return cart!.Products[Product!.Product];

            return 0;
        }

    }

    public ProductViewModel? Product
    {
        get => product;
        set
        {
            product = value;

            OnPropertyChanged();
        }
    }

    public CartViewModel? Cart
    {
        get => cart;
        set
        {
            cart = value;

            OnPropertyChanged();
        }
    }

    public AddToCartButton()
    {
        InitializeComponent();
    }

    public AddToCartButton(ProductViewModel productViewModel, CartViewModel cart) : this()
    {
        Product = productViewModel;
        Cart = cart;
    }

    private Visibility IsButtonVisible()
    {
        if (Cart!.Products.ContainsKey(Product!.Product))
            return Visibility.Collapsed;

        return Visibility.Visible;
    }

    private void AddButtonClick(object sender, RoutedEventArgs e)
    {
        Cart!.AddProduct(Product!.Product);

        ButtonClicked?.Invoke(this, new EventArgs());
        UpdateBinds();
    }

    private void IncrementButtonClick(object sender, RoutedEventArgs e)
    {
        Cart!.IncrementProduct(Product!.Product);

        ButtonClicked?.Invoke(this, new EventArgs());
        UpdateBinds();
    }

    private void DecrementButtonClick(object sender, RoutedEventArgs e)
    {
        Cart!.DecrementProduct(Product!.Product);

        ButtonClicked?.Invoke(this, new EventArgs());
        UpdateBinds();
    }

    private void UpdateBinds()
    {
        OnPropertyChanged(nameof(ButtonVisibility));
        OnPropertyChanged(nameof(AppenderVisibility));
        OnPropertyChanged(nameof(ProductInCartCount));
    }

    private void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
}
