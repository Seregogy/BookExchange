using System.Collections.Generic;

namespace BookExchange.Model;

public class Book
{
    private int id;

    private string name;
    private string description;
    private string genre;

    private string userExchangerName;
    
    private List<string> tags;

    private string pictureUri;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string Genre { get => genre; set => genre = value; }

    public string UserExchangerName { get => userExchangerName; set => userExchangerName = value; }

    public List<string> Tags { get => tags; set => tags = value; }
    
    public string PictureUri { get => pictureUri; set => pictureUri = value; }
}