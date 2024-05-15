using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance.Articles;

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

    public async Task<ErrorOr<Article>> Delete(Guid id)
    {
        Article? articleToDelete = await _dbContext.Articles.FindAsync(id);
        if (articleToDelete is null)
        {
            return Error.NotFound(description: $"Article is not found (article id: {id})");
        }
        _dbContext.Articles.Remove(articleToDelete);
        await _dbContext.SaveChangesAsync();
        return articleToDelete;
    }

    public async Task<ErrorOr<Article>> GetById(Guid id)
    {
        var article = await _dbContext.Articles
            .Include(ar => ar.Tags)
            .Include(ar => ar.User)
            .Include(ar => ar.ContentType)
            .Include(ar => ar.Category)
            .Include(ar => ar.Ratings)
            .FirstOrDefaultAsync(ar => ar.Id == id);
        if (article is null)
        {
            return Error.NotFound(description: $"Article is not found (article id: {id})");
        }
        return article;
    }

    public async Task<ErrorOr<IEnumerable<Article>>> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        Guid? userId = null,
        Guid? contentTypeId = null,
        Guid? categoryId = null,
        string? order = null,
        IEnumerable<Guid>? tagIds = null
    )
    {
        IQueryable<Article> query = _dbContext.Articles;

        if (userId is not null)
        {
            query = query.Where(a => a.UserId == userId);
        }
        if (contentTypeId is not null)
        {
            query = query.Where(a => a.ContentTypeId == contentTypeId);
        }
        if (categoryId is not null)
        {
            query = query.Where(a => a.CategoryId == categoryId);
        }
        if (tagIds is not null && tagIds.Any())
        {
            foreach (var tagId in tagIds)
            {
                query = query.Where(a => a.Tags.Select(t => t.Id).Contains(tagId));
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
                query = query.OrderByDescending(a => a.Published_Date);

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

    public async Task<ErrorOr<int>> GetTotalItems(Guid? userId = null)
    {
        if (userId is null)
        {
            return await _dbContext.Articles.CountAsync();
        }
        return await _dbContext.Articles.Where(ar => ar.UserId == userId).CountAsync();
    }

    public async Task<ErrorOr<Updated>> Update(Article updateArticle)
    {
        _dbContext.Articles.Update(updateArticle);
        await _dbContext.SaveChangesAsync();
        return Result.Updated;
    }
}
