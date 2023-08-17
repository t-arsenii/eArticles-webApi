using GamingBlog.API.Data;
using GamingBlog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingBlog.API.Services.Repositories;

public class DbMSSqlRepository : IArticlesRepository
{
    readonly GamingBlogDbContext _dbContext;

    public DbMSSqlRepository(GamingBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Article? Create(Article newArticle, IEnumerable<string>? tagNames = null)
    {
        if (tagNames != null && tagNames.Any())
        {
            foreach (var tagName in tagNames)
            {
                var tag = _dbContext.Tags.FirstOrDefault(t => t.Title == tagName);
                if (tag == null)
                {
                    tag = new Tag { Title = tagName };
                    _dbContext.Tags.Add(tag);
                }
                newArticle.Tags.Add(tag);
            }
        }
        newArticle.Published_Date = DateTime.Now;
        _dbContext.Articles.Add(newArticle);
        _dbContext.SaveChanges();
        return newArticle;
    }

    public Article? Delete(int id)
    {
        Article? articleToDelete = _dbContext.Articles.Find(id);
        if (articleToDelete == null)
        {
            return null;
        }
        _dbContext.Articles.Remove(articleToDelete);
        _dbContext.SaveChanges();
        return articleToDelete;
    }

    public Article? Get(int id)
    {
        return _dbContext.Articles.Include(ar => ar.Tags).FirstOrDefault(ar => ar.Id == id);
    }

    public List<string>? GetArticleTags(int id)
    {
        var article = Get(id);
        if (article == null)
        {
            return null;
        }
        var tags = article.Tags?.Select(t => t.Title).ToList();
        if (tags?.Count() == 0 || tags == null)
        {
            return null;
        }
        return tags;
    }

    public IEnumerable<Article>? GetPage(int currentPage = 1, int pageSize = 10)
    {
        return _dbContext.Articles
            .Include(ar => ar.ArticleTags)
            .ThenInclude(ar_tag => ar_tag.Tag)
            .OrderBy(ar => ar.Id)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }

    public Article? Update(Article updateArticle, IEnumerable<string>? tagNames = null)
    {
        Article? article = Get(updateArticle.Id);
        if(article == null)
        {
            return null;
        }
        if (tagNames != null && tagNames.Any())
        {
            foreach (var tagName in tagNames)
            {
                var tag = _dbContext.Tags.FirstOrDefault(t => t.Title == tagName);
                if (tag != null)
                {
                    updateArticle.Tags.Add(tag);
                }
            }
        }
        _dbContext.Articles.Update(updateArticle);
        _dbContext.SaveChanges();
        return updateArticle;
    }
}
