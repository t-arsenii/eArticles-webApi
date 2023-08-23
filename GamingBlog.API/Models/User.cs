using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Models;

public class User : IdentityUser
{
    public List<Article> Articles { get; } = new();
}
