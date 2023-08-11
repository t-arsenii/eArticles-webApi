namespace GamingBlog.API.Models;

public class ArticleTag
{
    public int ArticleId { get; set; }
    public Article _Article { get; set; }
    public int TagId { get; set;}
    public Tag _Tag { get; set;}
    
}
