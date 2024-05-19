namespace eArticles.API.Models;

public class Follower
{
    public Guid Id { get; set;}
    public Guid FollowingUserId { get; set; }
    public User? FollowingUser { get; set; }
    public Guid FollowedUserId { get; set; }
    public User? FollowedUser { get; set; }
    public DateTime DateAdded { get; set; }
}
