using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;

[ApiController]
[Route("/error")]
public class ErrorsHandler : ControllerBase
{
    public IActionResult Error()
    {
        return Problem();
    }
}
