using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Models;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<Article> Articles { get; } = new List<Article>();
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    public ICollection<Bookmark> Bookmarks { get; } = new List<Bookmark>();
    public ICollection<Follower> Followings { get; } = new List<Follower>();
    public ICollection<Follower> Followers { get; } = new List<Follower>();

}
 