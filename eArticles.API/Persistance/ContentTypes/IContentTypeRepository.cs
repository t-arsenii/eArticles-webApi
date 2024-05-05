using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance.ContentTypes;

public interface IContentTypeRepository
{
    public Task<ErrorOr<ContentType>> GetById(Guid id);
    public Task<ErrorOr<IEnumerable<ContentType>>> GetAll();
    public Task<ErrorOr<ContentType>> GetByTitle(string title);
    public Task<ErrorOr<ContentType>> Create(ContentType articleType);
    public Task<ErrorOr<ContentType>> Update(ContentType articleTypeUpdate);
    public Task<ErrorOr<ContentType>> Delete(Guid id);
}
