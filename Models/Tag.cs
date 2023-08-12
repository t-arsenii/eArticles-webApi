namespace GamingBlog.API.Models;

public class Tag
{
    public int Id { get; set;}
    public string Name { get; set;} = string.Empty;
    public ICollection<ArticleTag>? ArticleTags { get; set; }
}
