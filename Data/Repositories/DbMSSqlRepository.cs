using GamingBlog.API.Models;

namespace GamingBlog.API.Data.Repositories;

public class DbMSSqlRepository : IArticlesRepository
{
    readonly int PageSize = 5;
    readonly GamingBlogDbContext _dbContext;
    public DbMSSqlRepository(GamingBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(Article article)
    {
        article.Published_Date = DateTime.Now;
        _dbContext.Articles.Add(article);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        Article? articleToDelete = _dbContext.Articles.Find(id);
        if(articleToDelete != null)
        {
            _dbContext.Articles.Remove(articleToDelete);
            _dbContext.SaveChanges();
        }
    }

    public Article? Get(int id)
    {
        return _dbContext.Articles.Find(id);
    }

    public IEnumerable<Article>? GetPage(int currentPage = 1)
    {
        return _dbContext.Articles.OrderBy(ar => ar.Id).Skip((currentPage - 1) * PageSize).Take(PageSize);
    }

    public void Update(Article article)
    {
        _dbContext.Articles.Update(article);
    }
}
