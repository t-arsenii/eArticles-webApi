using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance;

public class ArticlesRepository : IArticlesRepository
{
    readonly eArticlesDbContext _dbContext;

    public ArticlesRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Article>> Create(Article newArticle, string contentType, string category, IEnumerable<string>? tagNames = null)
    {
        if (tagNames != null && tagNames.Any())
        {
            foreach (var tagName in tagNames)
            {
                Tag? tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Title == tagName);
                if (tag == null)
                {
                    return Error.NotFound(description: $"Tag is not found (tag title: {tagName})");
                }
                newArticle.Tags.Add(tag);
            }
        }
        var contentTypeEntity = await _dbContext.ArticleTypes.FirstOrDefaultAsync(aType => aType.Title == contentType);
        var articleCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Title == category);
        if (contentTypeEntity == null )
        {
            return Error.NotFound(description: $"ContentType is not found: (contentType title: {contentType})");
        }
        if (articleCategory == null)
        {

        }
        newArticle.ContentType = contentTypeEntity;
        newArticle.Category = articleCategory;
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
            .Include(ar => ar.ContentType)
            .Include(ar => ar.Category)
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
        string? contentType = null,
        string? category = null,
        string? order = null,
        string[]? tags = null
    )
    {
        IQueryable<Article> query = _dbContext.Articles;

        if (userId is not null)
        {
            query = query.Where(a => a.UserId == userId);
        }
        if (!string.IsNullOrEmpty(contentType))
        {
            query = query.Where(a => a.ContentType.Title.ToLower() == contentType.ToLower());
        }
        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(a => a.Category.Title.ToLower() == category.ToLower());
        }
        if (tags != null && tags.Any())
        {
            foreach (var tag in tags)
            {
                query = query.Where(a => a.Tags.Select(t => t.Title.ToLower()).Contains(tag.ToLower()));
            }
        }
        if (string.IsNullOrEmpty(order))
        {
            query = query.OrderBy(a => a.Id);
        }
        else
        {
            if (order.ToLower() == "date")
            {
                query = query.OrderBy(a => a.Published_Date);

            }
        }
        return await query
            .Include(a => a.Tags)
            .Include(a => a.User)
            .Include(a => a.ContentType)
            .Include(ar => ar.Category)
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

    public async Task<Article?> Update(Article updateArticle, string articleType, string category, IEnumerable<string>? tagNames = null)
    {
        Article? article = await GetById(updateArticle.Id);
        if (article == null)
        {
            return null;
        }
        _dbContext.Entry(article).State = EntityState.Detached;
        if (tagNames != null && tagNames.Any())
        {
            foreach (var tagName in tagNames)
            {
                var tag = _dbContext.Tags.FirstOrDefault(t => t.Title == tagName);
                if (tag == null)
                {
                    return null;
                }
            }
        }
        var articleTypeEntity = await _dbContext.ArticleTypes.FirstOrDefaultAsync(aType => aType.Title == articleType);
        var articleCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Title == category);
        if (articleTypeEntity == null || articleCategory == null)
        {
            return null;
        }
        updateArticle.ContentType = articleTypeEntity;
        updateArticle.Category = articleCategory;
        _dbContext.Articles.Update(updateArticle);
        await _dbContext.SaveChangesAsync();
        return updateArticle;
    }
}
