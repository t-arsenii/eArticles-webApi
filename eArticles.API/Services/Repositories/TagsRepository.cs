using eArticles.API.Data;
using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services.Repositories;

public class TagsRepository : ITagsRepository
{
    readonly eArticlesDbContext _dbContext;
    public TagsRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Tag?> Create(Tag newTag)
    {
        await _dbContext.Tags.AddAsync(newTag);
        await _dbContext.SaveChangesAsync();

        return newTag;
    }

    public async Task<Tag?> Delete(int id)
    {
        var tag = await _dbContext.Tags.FindAsync(id);
        if (tag == null)
        {
            return null;
        }
        _dbContext.Tags.Remove(tag);
        await _dbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<IEnumerable<Tag>?> GetAll()
    {
        return await _dbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetById(int id)
    {
        var tag = await _dbContext.Tags.FindAsync(id);
        if(tag == null)
        {
            return null;
        }
        return tag;
    }

    public async Task<Tag?> Update(Tag updateTag)
    {
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == updateTag.Id);
        if (tag == null)
        {
            return null;
        }
        _dbContext.Tags.Update(updateTag);
        await _dbContext.SaveChangesAsync();
        return updateTag;
    }
}
