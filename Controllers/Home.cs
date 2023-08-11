using Microsoft.AspNetCore.Mvc;

namespace GamingBlog.API.Controllers;

[Route("api/[controller]")]
public class Home : ControllerBase
{
    [HttpGet]
    public IActionResult Index(){
        return Ok();
    }
}
