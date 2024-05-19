using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApiController : ControllerBase
{
    private protected Guid GetLoggedUserId()
    {
        if (
            HttpContext.Items.TryGetValue("userId", out var userIdObj)
            && userIdObj is Guid userIdGuid
        )
        {
            return userIdGuid;
        }
        throw new ArgumentException("Error getting user id from HttpContext items");
    }
}
