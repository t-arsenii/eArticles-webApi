using eArticles.API.Models;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Persistance;

public interface IArticlesRepository
{
    public Task<ErrorOr<Article>> Create(Article newArticle);
    public Task<ErrorOr<Article>> GetById(int id);
    public Task<ErrorOr<IEnumerable<string>>> GetArticleTags(int id);
    public Task<ErrorOr<IEnumerable<Article>>> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        int? userId = null,
        string? contentType = null,
        string? category = null,
        string? order = null,
        string[]? tags = null
    );
    public Task<ErrorOr<int>> GetTotalItems(int? userId = null);
    public Task<ErrorOr<Article>> Update(Article updateArticle, string contentType, string category, IEnumerable<string>? tagNames = null);
    public Task<ErrorOr<Article>> Delete(int id);
}
