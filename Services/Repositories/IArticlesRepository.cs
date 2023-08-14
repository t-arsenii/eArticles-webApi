using GamingBlog.API.Models;

namespace GamingBlog.API.Services.Repositories;

public interface IArticlesRepository
{
    public Article? Create(Article article, List<string> tagNames);
    public Article? Get(int id);
    public List<String>? GetArticleTags(int id);
    public IEnumerable<Article>? GetPage(int currentPage = 1, int pageSize = 10);
    public void Update(Article article);
    public void Delete(int id);
}
