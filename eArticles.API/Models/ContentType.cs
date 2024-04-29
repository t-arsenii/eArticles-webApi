using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Models;

public class ContentType
{
    [Key]
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
