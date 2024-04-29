using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;

namespace eArticles.API.Services;

public class CategoryService : ICategoryService
{
    ICategoriesRepository _categoryRepository;

    public CategoryService(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Create(Category category)
    {
        var createCategoryResult = await _categoryRepository.Create(category);
        if (createCategoryResult.IsError)
        {
            return createCategoryResult.Errors;
        }
        return createCategoryResult.Value;
    }

    public async Task<ErrorOr<Category>> Delete(Guid id)
    {
        var deleteCategoryResult = await _categoryRepository.Delete(id);
        if (deleteCategoryResult.IsError)
        {
            return deleteCategoryResult.Errors;
        }
        return deleteCategoryResult.Value;
    }

    public async Task<ErrorOr<IEnumerable<Category>>> GetAll()
    {
        var getCategoriesResult = await _categoryRepository.GetAll();
        if (getCategoriesResult.IsError)
        {
            return getCategoriesResult.Errors;
        }
        return getCategoriesResult.Value.ToList();
    }

    public async Task<ErrorOr<Category>> GetById(Guid id)
    {
        var getCategoryResult = await _categoryRepository.GetById(id);
        if (getCategoryResult.IsError)
        {
            return getCategoryResult.Errors;
        }
        return getCategoryResult.Value;
    }

    public async Task<ErrorOr<Category>> GetByTitle(string title)
    {
        var getCategoryResult = await _categoryRepository.GetByTitle(title);
        if (getCategoryResult.IsError)
        {
            return getCategoryResult.Errors;
        }
        return getCategoryResult.Value;
    }

    public async Task<ErrorOr<Category>> Update(Category updateCategory)
    {
        var getCategoryResult = await _categoryRepository.GetById(updateCategory.Id);
        if (getCategoryResult.IsError)
        {
            return getCategoryResult.Errors;
        }
        var updateCategoryResult = await _categoryRepository.Update(updateCategory);
        if (updateCategoryResult.IsError)
        {
            return updateCategoryResult.Errors;
        }
        return updateCategoryResult.Value;
    }
}
