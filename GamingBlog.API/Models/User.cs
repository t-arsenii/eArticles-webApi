using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Models;

public class User : IdentityUser<int>
{
    public List<Article> Articles { get; } = new();
}
