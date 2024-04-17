using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services;

public interface IArticleService
{
    Task<ErrorOr<Article>> GetById(int id);

    public Task<ErrorOr<Article>> Create(
        Article newArticle,
        string contentType,
        string category,
        IEnumerable<string>? tagNames = null);
    public Task<ErrorOr<IEnumerable<Article>>> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        int? userId = null,
        string? contentType = null,
        string? category = null,
        string? order = null,
        string[]? tags = null);
    public Task<ErrorOr<int>> GetTotalItems(int? userId = null);
    public Task<ErrorOr<Article>> Update(
        Article updateArticle,
        string contentType,
        string category,
        IEnumerable<string>? tagNames = null);
    public Task<ErrorOr<Article>> Delete(int id);


    //public Task<IEnumerable<string>?> GetArticleTags(int id);
}
