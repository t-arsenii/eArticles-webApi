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

    public async Task<ErrorOr<Article>> Create(Article newArticle)
    {
        await _dbContext.Articles.AddAsync(newArticle);
        await _dbContext.SaveChangesAsync();
        return newArticle;
    }

    public async Task<ErrorOr<Article>> Delete(int id)
    {
        Article? articleToDelete = await _dbContext.Articles.FindAsync(id);
        if (articleToDelete == null)
        {
            return Error.NotFound(description: $"Article is not found (article id: {id})");
        }
        _dbContext.Articles.Remove(articleToDelete);
        await _dbContext.SaveChangesAsync();
        return articleToDelete;
    }

    public async Task<ErrorOr<Article>> GetById(int id)
    {
        var article = await _dbContext.Articles
            .Include(ar => ar.Tags)
            .Include(ar => ar.User)
            .Include(ar => ar.ContentType)
            .Include(ar => ar.Category)
            .FirstOrDefaultAsync(ar => ar.Id == id);
        if (article == null)
        {
            return Error.NotFound(description: $"Article is not found (article id: {id})");
        }
        return article;
    }

    public async Task<ErrorOr<IEnumerable<Article>>> GetPage(
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

    public async Task<ErrorOr<int>> GetTotalItems(int? userId = null)
    {
        if (userId == null)
        {
            return await _dbContext.Articles.CountAsync();
        }
        return await _dbContext.Articles.Where(ar => ar.UserId == userId).CountAsync();
    }

    public async Task<ErrorOr<Article>> Update(Article updateArticle)
    {
        _dbContext.Articles.Update(updateArticle);
        await _dbContext.SaveChangesAsync();
        return updateArticle;
    }
}
