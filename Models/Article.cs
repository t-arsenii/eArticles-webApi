namespace GamingBlog.API.Models;
using Data.Enums;
public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public ArticleType Article_type { get; set; }
    public DateTime Published_Date { get; set; }
    public ICollection<ArticleTag>? ArticleTags{ get; set;}
}
