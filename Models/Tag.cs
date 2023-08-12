using System.ComponentModel.DataAnnotations;

namespace GamingBlog.API.Models;

public class Tag
{
    [Key]
    public int Id { get; set;}
    public string Title { get; set;} = string.Empty;
    public ICollection<ArticleTag>? ArticleTags { get; set; }
}
