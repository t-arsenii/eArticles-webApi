using eArticles.API.Contracts.Article;
using eArticles.API.Contracts.Rating;
using eArticles.API.Models;
using eArticles.API.Services.Ratings;
using eArticles.API.Services.Users;
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
    IUserService _userService;

    public RatingsController(IRatingService ratingService, IUserService userService)
    {
        _ratingService = ratingService;
        _userService = userService;
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
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute(Name = "id")] Guid ratingId, [FromBody] UpdateRatingRequest ratingRequest)
    {
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var userHasAccessResult = await _ratingService.UserHasAccess(userId, ratingId);
        if (userHasAccessResult.IsError)
        {
            return NotFound(userHasAccessResult.FirstError.Description);
        }
        bool UserHasAccess = userHasAccessResult.Value;
        if (!UserHasAccess)
        {
            return Forbid();
        }
        var rating = new Rating()
        {
            Id = ratingId,
            UserId = userId,
            Value = ratingRequest.value
        };
        var updateRatingResult = await _ratingService.Update(rating);
        if (updateRatingResult.IsError)
        {
            return BadRequest(updateRatingResult.FirstError.Description);
        }
        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid ratingId)
    {
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var userHasAccessResult = await _ratingService.UserHasAccess(userId, ratingId);
        if (userHasAccessResult.IsError)
        {
            return NotFound(userHasAccessResult.FirstError.Description);
        }
        bool UserHasAccess = userHasAccessResult.Value;
        if (!UserHasAccess)
        {
            return Forbid();
        }
        var deleteRatingResult = await _ratingService.Delete(ratingId);
        if (deleteRatingResult.IsError)
        {
            return NotFound(deleteRatingResult.FirstError.Description);
        }
        return NoContent();
    }
}
