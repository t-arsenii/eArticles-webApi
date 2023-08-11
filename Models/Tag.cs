namespace GamingBlog.API.Models;

public class Tag
{
    public int Id { get; set;}
    public string Name { get; set;}
    public ICollection<ArticleTag> ArticleTags { get; set; }
}
