using eArticles.API.Data;
using eArticles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services.Repositories;

public class ContentTypeRepository : IContentTypeRepository
{
    eArticlesDbContext _dbContext;

    public ContentTypeRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentType> Create(ContentType articleType)
    {
        await _dbContext.ArticleTypes.AddAsync(articleType);
        await _dbContext.SaveChangesAsync();

        return articleType;
    }

    public async Task<ContentType?> Delete(int id)
    {
        var articleType = await _dbContext.ArticleTypes.FindAsync(id);
        if (articleType == null)
        {
            return null;
        }
        _dbContext.ArticleTypes.Remove(articleType);
        await _dbContext.SaveChangesAsync();
        return articleType;
    }

    public async Task<IEnumerable<ContentType>> GetAll()
    {
        return await _dbContext.ArticleTypes.ToListAsync();

    }

    public async Task<ContentType?> GetById(int id)
    {
        return await _dbContext.ArticleTypes.FindAsync(id);
    }

    public async Task<ContentType?> Update(ContentType contentTypeUpdate)
    {
        var contentType = await _dbContext.ArticleTypes.FindAsync(contentTypeUpdate.Id);
        if (contentType == null)
        {
            return null;
        }
        _dbContext.Entry(contentType).State = EntityState.Detached;
        _dbContext.ArticleTypes.Update(contentTypeUpdate);
        await _dbContext.SaveChangesAsync();
        return contentTypeUpdate;
    }
}
