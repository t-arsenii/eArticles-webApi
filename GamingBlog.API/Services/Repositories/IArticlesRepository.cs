using GamingBlog.API.Models;

namespace GamingBlog.API.Services.Repositories;

public interface IArticlesRepository
{
    public Task<Article?> Create(Article article, IEnumerable<string>? tagNames = null);
    public Task<Article?> Get(int id);
    public Task<IEnumerable<string>?> GetArticleTags(int id);
    public Task<IEnumerable<Article>?> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        int? userId = null
    );
    public Task<int> GetTotalItems(int? userId = null);
    public Task Update(Article updateArticle, IEnumerable<string>? tagNames = null);
    public Task<Article?> Delete(int id);
}
