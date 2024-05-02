using eArticles.API.Contracts.Article;
using eArticles.API.Contracts.Rating;
using eArticles.API.Models;
using eArticles.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eArticles.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RatingsController : ControllerBase
{
    IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRatingRequest ratingRequest)
    {
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var rating = new Rating()
        {
            ArticleId = ratingRequest.articleId,
            UserId = userId,
            Value = ratingRequest.value
        };
        var createRatingResult = await _ratingService.Create(rating);
        if (createRatingResult.IsError)
        {
            return BadRequest(createRatingResult.FirstError.Description);
        }
        var createdRating = createRatingResult.Value;
        return Ok(new RatingResponse(createdRating.Id, createdRating.Id, userId, createdRating.Value));
    }
}
