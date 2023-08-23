namespace GamingBlog.API.Models;

using System.ComponentModel.DataAnnotations;
using Data.Enums;

public class Article
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public ArticleType Article_type { get; set; }
    public DateTime Published_Date { get; set; }
    private string _img_Url = "https://placehold.co/100";
    public string Img_Url
    {
        set { _img_Url = value ?? _img_Url; }
        get { return _img_Url; }
    }
    public List<ArticleTag> ArticleTags { get; } = new();
    public List<Tag> Tags { get; } = new();
    public Guid UserId { get; set; }
    public User User { get; set; } = new();
}
