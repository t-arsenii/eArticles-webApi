using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Models;

public class Tag
{
    [Key]
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; } = new List<ArticleTag>();
    public ICollection<Article> Articles { get; } = new List<Article>();
}
