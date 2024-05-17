using eArticles.API.Models;
using eArticles.API.Persistance.Bookmarks;
using eArticles.API.Persistance.Users;
using eArticles.API.Services.Bookmarks;
using ErrorOr;

public class BookmarksService : IBookmarksService
{
    IBookmarksRepository _bookmarkRepository;
    IUsersRepository _usersRepository;

    public BookmarksService(IBookmarksRepository bookmarkRepository, IUsersRepository usersRepository)
    {
        _bookmarkRepository = bookmarkRepository;
        _usersRepository = usersRepository;
    }


    public async Task<ErrorOr<Bookmark>> Create(Bookmark bookmark)
    {
        var createBookmarkResult = await _bookmarkRepository.Create(bookmark);
        if (createBookmarkResult.IsError)
        {
            return createBookmarkResult.Errors;
        }
        return createBookmarkResult.Value;
    }

    public async Task<ErrorOr<Deleted>> DeleteBookmark(Guid bookmarkId)
    {
        var deleteBookmarkResult = await _bookmarkRepository.Delete(bookmarkId);
        if (deleteBookmarkResult.IsError)
        {
            return deleteBookmarkResult.Errors;
        }
        return Result.Deleted;
    }

    public async Task<ErrorOr<IEnumerable<Bookmark>>> GetUserBookmarks(User user)
    {
        var createBookmarkResult = await _bookmarkRepository.GetByUser(user.Id);
        if (createBookmarkResult.IsError)
        {
            return createBookmarkResult.Errors;
        }
        return createBookmarkResult.Value.ToList();
    }

    public async Task<ErrorOr<bool>> UserHasAccess(Guid userId, Guid bookmarkId)
    {
        var getUserResult = await _usersRepository.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return getUserResult.Errors;
        }
        var getArticleResult = await _bookmarkRepository.GetById(bookmarkId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        var article = getArticleResult.Value;
        var user = getUserResult.Value;
        if (article.UserId == user.Id)
        {
            return true;
        }
        return false;
    }
}
