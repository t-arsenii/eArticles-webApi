using eArticles.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Persistance;

public interface IArticlesRepository
{
    public Task<Article?> Create(Article newArticle, string contentType, string category, IEnumerable<string>? tagNames = null);
    public Task<Article?> GetById(int id);
    public Task<IEnumerable<string>?> GetArticleTags(int id);
    public Task<IEnumerable<Article>?> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        int? userId = null,
        string? contentType = null,
        string? category = null,
        string? order = null,
        string[]? tags = null
    );
    public Task<int> GetTotalItems(int? userId = null);
    public Task<Article?> Update(Article updateArticle, string contentType, string category, IEnumerable<string>? tagNames = null);
    public Task<Article?> Delete(int id);
}
