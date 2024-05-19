using System.Security.Claims;
using eArticles.API.Controllers;
using eArticles.API.Persistance.Bookmarks;
using eArticles.API.Services.Bookmarks;
using eArticles.API.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;

public class BookmarksController : ApiController
{
    private readonly IBookmarksService _bookmarksService;
    private readonly IUserService _userService;

    public BookmarksController(IBookmarksService bookmarkService, IUserService userService)
    {
        _bookmarksService = bookmarkService;
        _userService = userService;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateBookmark([FromRoute(Name = "id")] Guid articleId)
    {
        Guid userId = GetLoggedUserId();
        var bookmark = new Bookmark() { UserId = userId, ArticleId = articleId };
        var createBookmarkResult = await _bookmarksService.Create(bookmark);
        if (createBookmarkResult.IsError)
        {
            return BadRequest(createBookmarkResult.FirstError.Description);
        }
        return Ok();
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetUserBookmarks()
    {
        Guid userId = GetLoggedUserId();

        var getUserResult = await _userService.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return BadRequest(getUserResult.FirstError.Description);
        }
        var getBookmarsResult = await _bookmarksService.GetUserBookmarks(getUserResult.Value);
        if (getBookmarsResult.IsError)
        {
            return NotFound(getBookmarsResult.FirstError.Description);
        }
        var bookmarksDto = new List<BookmarkResponse>();
        foreach (var bookmark in getBookmarsResult.Value)
        {
            bookmarksDto.Add(
                new BookmarkResponse(
                    id: bookmark.Id,
                    userId: bookmark.Id,
                    articleId: bookmark.ArticleId,
                    dateAdded: bookmark.DateAdded.ToString()
                )
            );
        }
        return Ok(bookmarksDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookmark([FromRoute(Name = "id")] Guid bookmarkId)
    {
        Guid userId = GetLoggedUserId();

        var userHasAccessResult = await _bookmarksService.UserHasAccess(userId, bookmarkId);
        if (userHasAccessResult.IsError)
        {
            return Conflict(userHasAccessResult.FirstError.Description);
        }
        if (!userHasAccessResult.Value)
        {
            return Forbid();
        }
        var deleteBookmarkResult = await _bookmarksService.DeleteBookmark(bookmarkId);
        if (deleteBookmarkResult.IsError)
        {
            return NotFound(deleteBookmarkResult.FirstError.Description);
        }
        return NoContent();
    }
}
