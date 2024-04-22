using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services;

public interface ITagsService
{
    Task<ErrorOr<Tag>> Create(Tag tag);
    Task<ErrorOr<Tag>> Update(Tag tag);
    Task<ErrorOr<Tag>> Delete(Guid id);
    Task<ErrorOr<Tag>> GetById(Guid id);
    Task<ErrorOr<Tag>> GetByTitle(string title);
    Task<ErrorOr<IEnumerable<Tag>>> GetAll();
}
