using BookExchange.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BookExchange.View;

public sealed partial class BookCard : UserControl, INotifyPropertyChanged
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
    
    private Action<object, Book?, UIElement>? onClick;

    public BookCard()
    {
        InitializeComponent();
    }

    public BookCard(Book book, Action<object, Book?, UIElement> onClick = null) : this()
    {
        Book = book;

        this.onClick = onClick;
    }

    public void OnPropertyChanged([CallerMemberName] string property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    private void Grid_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        onClick?.Invoke(this, book, Backdrop);
    }
}