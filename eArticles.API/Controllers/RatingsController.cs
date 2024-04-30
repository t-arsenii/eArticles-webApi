using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;
[ApiController]
[Route("api/rateArticle")]
public class RatingsController : ControllerBase
{
    [HttpPost("{articleId}/{value}")]
    IActionResult rateArticle([FromRoute] int articleId, [FromRoute] int value)
    {
        return Ok();
    }
}
