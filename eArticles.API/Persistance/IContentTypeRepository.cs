using eArticles.API.Models;

namespace eArticles.API.Persistance;

public interface IContentTypeRepository
{
    public Task<ContentType?> GetById(int id);
    public Task<IEnumerable<ContentType>> GetAll();
    public Task<ContentType> Create(ContentType articleType);
    public Task<ContentType?> Update(ContentType articleTypeUpdate);
    public Task<ContentType?> Delete(int id);
}
