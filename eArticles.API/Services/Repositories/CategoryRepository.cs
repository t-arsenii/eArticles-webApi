using eArticles.API.Data;
using eArticles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services.Repositories;

public class CategoryRepository : ICategoryRepository
{
    eArticlesDbContext _dbContext;
    public CategoryRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Category?> Create(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        _dbContext.SaveChanges();
        return category;
    }

    public async Task<Category?> Delete(int id)
    {
        var categoryToDelete = await _dbContext.Categories.FindAsync(id);
        if (categoryToDelete == null)
        {
            return null;
        }
        _dbContext.Categories.Remove(categoryToDelete);
        await _dbContext.SaveChangesAsync();
        return categoryToDelete;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        IEnumerable<Category> categories = await _dbContext.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetById(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        return category;
    }

    public async Task<Category?> Update(Category updateCategory)
    {
        var category = GetById(updateCategory.Id);
        if (category == null)
        {
            return null;
        }
        _dbContext.Categories.Update(updateCategory);
        await _dbContext.SaveChangesAsync();
        return updateCategory;

    }
}
