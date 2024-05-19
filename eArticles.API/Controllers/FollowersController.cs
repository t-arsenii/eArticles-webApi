using eArticles.API.Models;
using eArticles.API.Services.Followers;
using eArticles.API.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;

public class FollowersController : ApiController
{
    IUserService _userService;
    IFollowersService _followersService;

    public FollowersController(IUserService userService, IFollowersService followersService)
    {
        _userService = userService;
        _followersService = followersService;
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> FollowUser([FromRoute(Name = "id")] Guid followingUserId)
    {
        Guid loggedUserId = GetLoggedUserId();
        var getFollowingUserResult = await _userService.GetUserById(followingUserId);
        if (getFollowingUserResult.IsError)
        {
            return NotFound(getFollowingUserResult.FirstError.Description);
        }
        var follower = new Follower()
        {
            FollowedUserId = loggedUserId,
            FollowingUserId = followingUserId,
        };
        var createFollowerResult = await _followersService.Create(follower);
        if (createFollowerResult.IsError)
        {
            return NotFound(createFollowerResult.FirstError.Description);
        }
        return Ok(
            new FollowerResponse(
                followingUserId: createFollowerResult.Value.FollowingUserId,
                followedUserId: createFollowerResult.Value.FollowedUserId
            )
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> UnfollowUser([FromRoute(Name = "id")] Guid followingUserId)
    {
        Guid loggedUserId = GetLoggedUserId();
        var getFollowingUserResult = await _userService.GetUserById(followingUserId);
        if (getFollowingUserResult.IsError)
        {
            return NotFound(getFollowingUserResult.FirstError.Description);
        }
        var deleteFollowerResult = await _followersService.Delete(loggedUserId, followingUserId);
        if (deleteFollowerResult.IsError)
        {
            return NotFound(deleteFollowerResult.FirstError.Description);
        }
        return NoContent();
    }
}
