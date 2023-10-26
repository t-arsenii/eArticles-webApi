namespace eArticles.API.Models;

public class ArticleTag
{
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public int TagId { get; set;}
    public Tag Tag { get; set;} = null!;
    
}
