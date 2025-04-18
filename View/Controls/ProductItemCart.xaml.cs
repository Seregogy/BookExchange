using Store.ViewModel;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace Store.View.Controls;

public sealed partial class ProductItemCart : UserControl
{
    private CartViewModel? cart;
    private ProductViewModel? product;

    private Action<object, ProductViewModel>? click;
    private Action<object, ProductViewModel>? additionalButtonClick;
    private Action<object, ProductViewModel>? remove;

    public ProductViewModel? Product { get => product; private set => product = value; }
    public CartViewModel? Cart { get => cart; private set => cart = value; }

    public ProductItemCart()
    {
        InitializeComponent();
    }

    public ProductItemCart 
    (  
        ProductViewModel product, 
        Action<object, ProductViewModel>? click = null,
        Action<object, ProductViewModel>? additionalButtonClick = null,
        Action<object, ProductViewModel>? remove = null
    ) : this()
    {
        Cart = CartViewModel.Init();
        Product = product;

        this.click = click;
        this.additionalButtonClick = additionalButtonClick;
        this.remove = remove;
    }

    private void LaunchProductPage(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e) =>
        click?.Invoke(sender, Product!);

    private void AddToCartButton_ButtonClicked(object sender, EventArgs e)
    {
        additionalButtonClick?.Invoke(this, Product!);

        if (sender is AddToCartButton button && button.Cart!.CountOf(Product!.Product) - 1 < 0)
            remove?.Invoke(sender, Product!);
    }
}