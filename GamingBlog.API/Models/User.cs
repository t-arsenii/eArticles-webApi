using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Models;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<Article> Articles { get; } = new();
}
