using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance.Bookmarks;

public class BookmarksRepository : IBookmarksRepository
{
    eArticlesDbContext _dbContext;

    public BookmarksRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Bookmark>> Create(Bookmark bookmark)
    {
        await _dbContext.Bookmarks.AddAsync(bookmark);
        await _dbContext.SaveChangesAsync();
        return bookmark;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid bookmarkId)
    {
        var getBookmarkResult = await GetById(bookmarkId);
        if (getBookmarkResult.IsError)
        {
            return getBookmarkResult.Errors;
        }
        _dbContext.Bookmarks.Remove(getBookmarkResult.Value);
        await _dbContext.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<Bookmark>> GetById(Guid bookmarkId)
    {
        var bookmark = await _dbContext.Bookmarks.FindAsync(bookmarkId);
        if (bookmark is null)
        {
            return Error.NotFound(description: $"Bookmark is not found(bookmarkId: {bookmarkId}");
        }
        return bookmark;
    }

    public async Task<ErrorOr<IEnumerable<Bookmark>>> GetByUser(Guid userId)
    {
        var bookmarks = await _dbContext.Bookmarks.Where(e => e.UserId == userId).ToListAsync();
        if (bookmarks is null || !bookmarks.Any())
        {
            return Error.NotFound(
                description: $"Bookmarks are not found for specified user(userId: {userId}"
            );
        }
        return bookmarks;
    }

    public async Task<ErrorOr<Bookmark>> GetByUserAndArticle(Guid userId, Guid articldeId)
    {
        var bookmark = await _dbContext.Bookmarks
            .Where(e => e.UserId == userId)
            .Where(e => e.ArticleId == articldeId)
            .FirstOrDefaultAsync();
        if (bookmark is null)
        {
            return Error.NotFound(description: "");
        }
        return bookmark;
    }
}
