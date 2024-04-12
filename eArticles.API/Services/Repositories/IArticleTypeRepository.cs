using eArticles.API.Models;

namespace eArticles.API.Services.Repositories;

public interface IArticleTypeRepository
{
    public Task<ArticleType?> GetById(int id);
    public Task<IEnumerable<ArticleType>> GetAll();
    public Task<ArticleType> Create(ArticleType articleType);
    public Task<ArticleType?> Update(ArticleType articleTypeUpdate);
    public Task<ArticleType?> Delete(int id);
}
