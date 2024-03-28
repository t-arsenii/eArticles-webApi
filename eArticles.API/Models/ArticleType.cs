using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Models;

public class ArticleType
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
