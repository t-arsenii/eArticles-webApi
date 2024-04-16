using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance;

public interface ICategoryRepository
{
    public Task<ErrorOr<Category>> GetById(int id);
    public Task<ErrorOr<Category>> GetByTitle(string title);
    public Task<ErrorOr<IEnumerable<Category>>> GetAll();
    public Task<ErrorOr<Category>> Create(Category category);
    public Task<ErrorOr<Category>> Update(Category updateCategory);
    public Task<ErrorOr<Category>> Delete(int id);
}
