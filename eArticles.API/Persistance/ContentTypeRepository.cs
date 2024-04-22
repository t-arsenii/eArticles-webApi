using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance;

public class ContentTypeRepository : IContentTypeRepository
{
    eArticlesDbContext _dbContext;

    public ContentTypeRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<ContentType>> Create(ContentType articleType)
    {
        await _dbContext.ContentTypes.AddAsync(articleType);
        await _dbContext.SaveChangesAsync();

        return articleType;
    }

    public async Task<ErrorOr<ContentType>> Delete(Guid id)
    {
        var articleType = await _dbContext.ContentTypes.FindAsync(id);
        if (articleType is null)
        {
            return Error.NotFound(description: $"ContentType is not found (contentType id: {id})");
        }
        _dbContext.ContentTypes.Remove(articleType);
        await _dbContext.SaveChangesAsync();
        return articleType;
    }

    public async Task<ErrorOr<IEnumerable<ContentType>>> GetAll()
    {
        var contentTypes = await _dbContext.ContentTypes.ToListAsync();
        if (!contentTypes.Any())
        {
            return Error.NotFound(description: $"ContentTypes are not found");
        }
        return contentTypes;

    }

    public async Task<ErrorOr<ContentType>> GetById(Guid id)
    {
        var contentType = await _dbContext.ContentTypes.FindAsync(id);
        if (contentType is null)
        {
            return Error.NotFound(description: $"ContentType is not found (contentType id: {id})");
        }
        return contentType;
    }

    public async Task<ErrorOr<ContentType>> GetByTitle(string title)
    {
        var contentType = await _dbContext.ContentTypes.FirstOrDefaultAsync(c => c.Title == title);
        if (contentType is null)
        {
            return Error.NotFound(description: $"ContentType is not ContentType (ContentType title: {title})");
        }
        return contentType;
    }

    public async Task<ErrorOr<ContentType>> Update(ContentType contentTypeUpdate)
    {
        var contentType = await _dbContext.ContentTypes.FindAsync(contentTypeUpdate.Id);
        if (contentType is null)
        {
            return Error.NotFound(description: $"ContentType is not found (contentType id: {contentTypeUpdate.Id})");

        }
        _dbContext.Entry(contentType).State = EntityState.Detached;
        _dbContext.ContentTypes.Update(contentTypeUpdate);
        await _dbContext.SaveChangesAsync();
        return contentTypeUpdate;
    }
}
