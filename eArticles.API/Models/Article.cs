namespace eArticles.API.Models;

using Data.Enums;
using System.ComponentModel.DataAnnotations;

public class Article
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int ArticleTypeId { get; set; }
    public ArticleType? ArticleType { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime Published_Date { get; set; }
    private string _imgUrl = "https://placehold.co/100";
    public string Img_Url
    {
        set { _imgUrl = value ?? _imgUrl; }
        get { return _imgUrl; }
    }
    public List<ArticleTag> ArticleTags { get; } = new();
    public List<Tag> Tags { get; } = new();
    public int? UserId { get; set; }
    public User? User { get; set; }
}
