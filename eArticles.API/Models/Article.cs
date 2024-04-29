namespace eArticles.API.Models;

using System.ComponentModel.DataAnnotations;
public class Article
{
    [Key]
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public Guid ContentTypeId { get; set; }
    public ContentType? ContentType { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime Published_Date { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    //private string _imgUrl = "https://placehold.co/100";
    //public string Img_Url
    //{
    //    set { _imgUrl = value ?? _imgUrl; }
    //    get { return _imgUrl; }
    //}
    public string? ImagePath { get; set; }
}
