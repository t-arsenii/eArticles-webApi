using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<ArticleTag> ArticleTags { get; } = new();
    public List<Article> Articles { get; } = new();
}
