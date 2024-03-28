using eArticles.API.Data;
using eArticles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services.Repositories;

public class ArticlesRepository : IArticlesRepository
{
    readonly eArticlesDbContext _dbContext;

    public ArticlesRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article?> Create(Article newArticle, IEnumerable<string>? tagNames = null)
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
        await _dbContext.Articles.AddAsync(newArticle);
        await _dbContext.SaveChangesAsync();
        return newArticle;
    }

    public async Task<Article?> Delete(int id)
    {
        Article? articleToDelete = await _dbContext.Articles.FindAsync(id);
        if (articleToDelete == null)
        {
            return null;
        }
        _dbContext.Articles.Remove(articleToDelete);
        await _dbContext.SaveChangesAsync();
        return articleToDelete;
    }

    public async Task<Article?> GetById(int id)
    {
        return await _dbContext.Articles
            .Include(ar => ar.Tags)
            .Include(ar => ar.User)
            .FirstOrDefaultAsync(ar => ar.Id == id);
    }

    public async Task<IEnumerable<string>?> GetArticleTags(int id)
    {
        var article = await GetById(id);

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

    public async Task<IEnumerable<Article>?> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        int? userId = null,
        string articleType = "",
        string order = "",
        string[]? tags = null
    )
    {
        IQueryable<Article> query = _dbContext.Articles;

        if(userId is not null)
        {
            query = query.Where(a => a.UserId == userId);
        }
        if(!string.IsNullOrEmpty(articleType))
        {
            query = query.Where(a => a.ArticleType.Title.ToLower() == articleType.ToLower());
        }
        if(!string.IsNullOrEmpty(order))
        {
            if(order.ToLower() == "date")
            {
                query = query.OrderBy(a => a.Published_Date);

            }
        }
        if(tags != null && tags.Any())
        {
            query = query.Where(a => a.Tags.Select(t => t.Title).SequenceEqual(tags));
        }
        return await query
            .Include(a => a.Tags)
            .Include(a => a.User)
            .Include(a => a.ArticleType)
            .OrderBy(ar => ar.Id)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalItems(int? userId = null)
    {
        if (userId == null)
        {
            return await _dbContext.Articles.CountAsync();
        }
        return await _dbContext.Articles.Where(ar => ar.UserId == userId).CountAsync();
    }

    public async Task<Article?> Update(Article updateArticle, IEnumerable<string>? tagNames = null)
    {
        Article? article = await GetById(updateArticle.Id);
        if (article == null)
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
        await _dbContext.SaveChangesAsync();
        return updateArticle;
    }
}
