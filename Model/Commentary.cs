namespace Store.Model;

public class Commentary
{
    public string UserId { get; set; }
    public string UserName { get; set; }

    public string Content { get; set; }
    
    public long Time { get; set; }
    public int Likes { get; set; }
}