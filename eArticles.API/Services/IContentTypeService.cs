using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services;

public interface IContentTypeService
{
    public Task<ErrorOr<ContentType>> GetById(Guid id);
    public Task<ErrorOr<IEnumerable<ContentType>>> GetAll();
    public Task<ErrorOr<ContentType>> GetByTitle(string title);
    public Task<ErrorOr<ContentType>> Create(ContentType contentType);
    public Task<ErrorOr<ContentType>> Update(ContentType contentTypeUpdate);
    public Task<ErrorOr<ContentType>> Delete(Guid id);
}
