using eArticles.API.Data;
using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;

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
        await _dbContext.AddRangeAsync();
        return tag;
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
        var tag = await _dbContext.Tags.FindAsync(updateTag.Id);
        if (tag == null)
        {
            return null;
        }
        _dbContext.Tags.Update(tag);
        await _dbContext.SaveChangesAsync();
        return updateTag;
    }
}
