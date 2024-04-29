using eArticles.API.Data;
using eArticles.API.Contracts.User;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance;

public class TagsRepository : ITagsRepository
{
    readonly eArticlesDbContext _dbContext;
    public TagsRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Tag>> Create(Tag newTag)
    {
        await _dbContext.Tags.AddAsync(newTag);
        await _dbContext.SaveChangesAsync();

        return newTag;
    }


    public async Task<ErrorOr<Tag>> Delete(Guid id)
    {
        var tag = await _dbContext.Tags.FindAsync(id);
        if (tag is null)
        {
            return Error.NotFound(description: $"Tag is not found (tag id: {id})");
        }
        _dbContext.Tags.Remove(tag);
        await _dbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<ErrorOr<IEnumerable<Tag>>> GetAll()
    {
        var tags = await _dbContext.Tags.ToListAsync();
        if (!tags.Any())
        {
            return Error.NotFound(description: $"Tags are not found");
        }
        return tags;
    }

    public async Task<ErrorOr<Tag>> GetById(Guid id)
    {
        var tag = await _dbContext.Tags.FindAsync(id);
        if (tag is null)
        {
            return Error.NotFound(description: $"Tag is not found (tag id: {id})");
        }
        return tag;
    }

    public async Task<ErrorOr<Tag>> GetByTitle(string title)
    {
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Title == title);
        if(tag is null)
        {
            return Error.NotFound(description: $"Tag is not found (tag title: {title})");
        }
        return tag;
    }

    public async Task<ErrorOr<Tag>> Update(Tag updateTag)
    {
        _dbContext.Tags.Update(updateTag);
        await _dbContext.SaveChangesAsync();
        return updateTag;
    }
}
