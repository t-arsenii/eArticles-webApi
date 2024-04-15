using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;

namespace eArticles.API.Services;

public class ArticleService : IArticleService
{
    IArticlesRepository _articlesRepository;
    public ArticleService(IArticlesRepository articlesRepository)
    {
        _articlesRepository = articlesRepository;
    }

    public async Task<ErrorOr<Article?>> Create(Article newArticle, string contentType, string category, IEnumerable<string>? tagNames = null)
    {
        
    }

    public Task<ErrorOr<Article>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Article>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<IEnumerable<Article>>> GetPage(int currentPage = 1, int pageSize = 10, int? userId = null, string? contentType = null, string? category = null, string? order = null, string[]? tags = null)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<int>> GetTotalItems(int? userId = null)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Article>> Update(Article updateArticle, string contentType, string category, IEnumerable<string>? tagNames = null)
    {
        throw new NotImplementedException();
    }
}
