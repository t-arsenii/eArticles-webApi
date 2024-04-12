namespace eArticles.API.Models;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public Article? Article { get; set; }
}
