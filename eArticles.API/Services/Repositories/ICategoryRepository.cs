using eArticles.API.Models;

namespace eArticles.API.Services.Repositories;

public interface ICategoryRepository
{
    public Task<Category?> GetById(int id);
    public Task<IEnumerable<Category>> GetAll();
    public Task<Category?> Create(Category category);
    public Task<Category?> Update(Category updateCategory);
    public Task<Category?> Delete(int id);
}
