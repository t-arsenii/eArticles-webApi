using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance.Categories;

public class CategoriesRepository : ICategoriesRepository
{
    eArticlesDbContext _dbContext;
    public CategoriesRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<Category>> Create(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        _dbContext.SaveChanges();
        return category;
    }

    public async Task<ErrorOr<Category>> Delete(Guid id)
    {
        var categoryToDelete = await _dbContext.Categories.FindAsync(id);
        if (categoryToDelete is null)
        {
            return Error.NotFound(description: $"Category is not found (category id: {id})");
        }
        _dbContext.Categories.Remove(categoryToDelete);
        await _dbContext.SaveChangesAsync();
        return categoryToDelete;
    }

    public async Task<ErrorOr<IEnumerable<Category>>> GetAll()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        if (!categories.Any())
        {
            return Error.NotFound(description: $"Categories are not found");
        }
        return categories;
    }

    public async Task<ErrorOr<Category>> GetById(Guid id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return Error.NotFound(description: $"Category is not found (category id: {id})");
        }
        return category;
    }

    public async Task<ErrorOr<Category>> GetByTitle(string title)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Title == title);
        if (category is null)
        {
            return Error.NotFound(description: $"Category is not found (category title: {title})");
        }
        return category;
    }

    public async Task<ErrorOr<Category>> Update(Category updateCategory)
    {
        _dbContext.Categories.Update(updateCategory);
        await _dbContext.SaveChangesAsync();
        return updateCategory;

    }
}
