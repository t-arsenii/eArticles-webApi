using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance.Bookmarks;

public interface IBookmarksRepository
{
    Task<ErrorOr<Bookmark>> GetById(Guid bookmarkId);
    Task<ErrorOr<IEnumerable<Bookmark>>> GetByUser(Guid userId);
    Task<ErrorOr<Bookmark>> GetByUserAndArticle(Guid userId, Guid articldeId);
    Task<ErrorOr<Bookmark>> Create(Bookmark bookmark);
    Task<ErrorOr<Deleted>> Delete(Guid bookmarkId);
}
