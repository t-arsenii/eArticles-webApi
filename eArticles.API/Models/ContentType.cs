using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Models;

public class ContentType
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<Article> Articles { get; set; } = new();
}
