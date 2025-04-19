using System.Collections.Generic;
using System.Text;

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

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Книга: {Name}");
        sb.AppendLine($"ID: {Id}");
        sb.AppendLine($"Жанр: {Genre}");
        sb.AppendLine($"Владелец: {UserExchangerName}");
        sb.AppendLine($"Описание: {Description}");

        if (Tags != null && Tags.Count > 0)
        {
            sb.AppendLine($"Теги: {string.Join(", ", Tags)}");
        }

        return sb.ToString();
    }

    public override bool Equals(object? obj) =>
        (obj is Book book) && book.Id == this.Id;

    public override int GetHashCode() => Id;
}