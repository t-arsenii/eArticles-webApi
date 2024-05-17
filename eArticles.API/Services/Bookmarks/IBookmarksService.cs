using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services.Bookmarks;

public interface IBookmarksService
{
    Task<ErrorOr<Bookmark>> Create(Bookmark bookmark);
    Task<ErrorOr<IEnumerable<Bookmark>>> GetUserBookmarks(User user);
    Task<ErrorOr<Deleted>> DeleteBookmark(Guid bookmarkId);
    Task<ErrorOr<bool>> UserHasAccess(Guid userId, Guid bookmarkId);
}
