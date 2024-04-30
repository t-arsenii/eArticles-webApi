namespace eArticles.API.Models;

public class Rating
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Guid UserId { get; set; }
    public int Value { get; set; }
}
