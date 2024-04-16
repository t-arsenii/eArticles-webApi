﻿using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance;

public class CategoryRepository : ICategoryRepository
{
    eArticlesDbContext _dbContext;
    public CategoryRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<Category>> Create(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        _dbContext.SaveChanges();
        return category;
    }

    public async Task<ErrorOr<Category>> Delete(int id)
    {
        var categoryToDelete = await _dbContext.Categories.FindAsync(id);
        if (categoryToDelete == null)
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
        if(!categories.Any())
        {
            return Error.NotFound(description: $"Categories are not found");
        }
        return categories;
    }

    public async Task<ErrorOr<Category>> GetById(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if(category == null)
        {
            return Error.NotFound(description: $"Category is not found (category id: {id})");
        }
        return category;
    }

    public async Task<ErrorOr<Category>> GetByTitle(string title)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Title == title);
        if(category == null)
        {
            return Error.NotFound(description: $"Category is not found (category title: {title})");
        }
        return category;
    }

    public async Task<ErrorOr<Category>> Update(Category updateCategory)
    {
        var category = await _dbContext.Categories.FindAsync(updateCategory.Id);
        if (category == null)
        {
            return Error.NotFound(description: $"Category is not found (category id: {updateCategory.Id})");
        }
        _dbContext.Entry(category).State = EntityState.Detached;
        _dbContext.Categories.Update(updateCategory);
        await _dbContext.SaveChangesAsync();
        return updateCategory;

    }
}
