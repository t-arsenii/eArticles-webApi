using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services.Articles;

public interface IArticleService
{
    Task<ErrorOr<Article>> GetById(Guid id);
    public Task<ErrorOr<Article>> Create(
        Article newArticle,
        IEnumerable<Guid>? tagIds = null);
    public Task<ErrorOr<IEnumerable<Article>>> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        Guid? userId = null,
        Guid? contentTypeId = null,
        Guid? categoryId = null,
        string? order = null,
        IEnumerable<Guid>? tagIds = null);
    public Task<ErrorOr<int>> GetTotalItems(Guid? userId = null);
    public Task<ErrorOr<Article>> Update(
        Article updateArticle,
        IEnumerable<Guid>? tagIds = null);
    public Task<ErrorOr<Article>> Delete(Guid id);
    Task<ErrorOr<bool>> UserHasAccess(Guid userId, Guid ratingId);
}
