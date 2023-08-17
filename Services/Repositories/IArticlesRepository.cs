using GamingBlog.API.Models;

namespace GamingBlog.API.Services.Repositories;

public interface IArticlesRepository
{
    public Article? Create(Article article, IEnumerable<string>? tagNames = null);
    public Article? Get(int id);
    public List<String>? GetArticleTags(int id);
    public IEnumerable<Article>? GetPage(int currentPage = 1, int pageSize = 10);
    public Article? Update(Article updateArticle, IEnumerable<string>? tagNames = null);
    public Article? Delete(int id);
}
