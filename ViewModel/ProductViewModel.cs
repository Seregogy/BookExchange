using Store.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Store.ViewModel;

public class ProductViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private Product product { get; set; } = new Product()
    {
        Name = "NULL",
        Description = "NULL",
        Category = "NULL",
        Provider = "ИП Ибрагимов",
        Rating = 4.7f,
        Commentaries = [],
        Specs = [],
        Tags = [ "Default" ],
        Pictures = ["ms-appx:///Resources/chocolate_cake.jpg", "ms-appx:///Resources/chocolate_cake2.jpg"]
    };

    public ProductViewModel() { }

    public ProductViewModel(Product product)
    {
        this.product = product;
    }

    public int Id 
    { 
        get => product.Id;
        set
        {
            product.Id = value;

            OnProperyChanged();
        }
    }

    public Product Product
    {
        get => product;
        set => product = value;
    }

    public List<string> Pictures
    {
        get => product.Pictures;
        set
        {
            product.Pictures = value;

            OnProperyChanged();
        }
    }

    public string Name
    {
        get => product.Name;
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            product.Name = value;

            OnProperyChanged();
        }
    }

    public string Description
    {
        get => product.Description;
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            product.Description = value; 
            
            OnProperyChanged();
        }
    }

    public List<Spec> Specs
    {
        get => product.Specs;
        set
        {
            product.Specs = value;

            OnProperyChanged();
        }
    }

    public string Category
    {
        get => product.Category;
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            product.Category = value;

            OnProperyChanged();
        }
    }

    public double Cost
    {
        get => product.Cost;
        set
        {
            product.Cost = value;

            OnProperyChanged();
        }
    }

    public List<string> Tags
    {
        get => product.Tags;
        set
        {
            product.Tags = value;

            OnProperyChanged();
        }
    }

    public string Provider
    {
        get => product.Provider;
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            product.Provider = value;

            OnProperyChanged();
        }
    }

    public float Rating
    {
        get => product.Rating;
        set
        {
            product.Rating = value;

            OnProperyChanged();
        }
    }

    public List<Commentary> Commentaries
    {
        get => product.Commentaries;
        set 
        { 
            product.Commentaries = value; 
            
            OnProperyChanged();
        }
    }

    public void AddComment(Commentary commentary)
    {
        product.Commentaries.Add(commentary);
        OnProperyChanged("Commentaries");
    }

    public void RemoveCommentary(int index)
    {
        if (index < 0 || product.Commentaries.Count < index) return;

        product.Commentaries.RemoveAt(index);
        OnProperyChanged("Commentaries");
    }

    public void RemoveCommentary(string content)
    {
        var targetCommentary = product.Commentaries.Where(x => x.Content.Contains(content)).First();

        if (targetCommentary == null) return;
        product.Commentaries.Remove(targetCommentary);

        OnProperyChanged("Commentaries");
    }

    public List<Commentary> GetAllCommentaries() =>
        product.Commentaries;

    private void OnProperyChanged([CallerMemberName] string property = "") => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
}