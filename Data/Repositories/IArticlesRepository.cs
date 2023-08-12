using GamingBlog.API.Models;

namespace GamingBlog.API.Data.Repositories;

public interface IArticlesRepository
{
    public void Create(Article article);
    public Article? Get(int id);
    public IEnumerable<Article>? GetPage(int page);
    public void Update(Article article);
    public void Delete(int id);
}
