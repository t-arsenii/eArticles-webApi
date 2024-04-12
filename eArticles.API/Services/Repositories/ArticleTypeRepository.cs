using eArticles.API.Data;
using eArticles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services.Repositories;

public class ArticleTypeRepository : IArticleTypeRepository
{
    eArticlesDbContext _dbContext;

    public ArticleTypeRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleType> Create(ArticleType articleType)
    {
        await _dbContext.ArticleTypes.AddAsync(articleType);
        await _dbContext.SaveChangesAsync();

        return articleType;
    }

    public async Task<ArticleType?> Delete(int id)
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

    public async Task<IEnumerable<ArticleType>> GetAll()
    {
        return await _dbContext.ArticleTypes.ToListAsync();

    }

    public async Task<ArticleType?> GetById(int id)
    {
        return await _dbContext.ArticleTypes.FindAsync(id);
    }

    public async Task<ArticleType?> Update(ArticleType articleTypeUpdate)
    {
        var articleType = await _dbContext.ArticleTypes.FindAsync(articleTypeUpdate.Id);
        if (articleType == null)
        {
            return null;
        }
        _dbContext.ArticleTypes.Update(articleTypeUpdate);
        await _dbContext.SaveChangesAsync();
        return articleType;
    }
}
