using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;

namespace eArticles.API.Services;

public class ContentTypeService : IContentTypeService
{
    IContentTypeRepository _contentTypeRepository;

    public ContentTypeService(IContentTypeRepository contentTypeRepository)
    {
        _contentTypeRepository = contentTypeRepository;
    }

    public async Task<ErrorOr<ContentType>> Create(ContentType contentType)
    {
        var createContentTypeResult = await _contentTypeRepository.Create(contentType);
        if (createContentTypeResult.IsError)
        {
            return createContentTypeResult.Errors;
        }
        return createContentTypeResult;
    }

    public async Task<ErrorOr<ContentType>> Delete(Guid id)
    {
        var createContentTypeResult = await _contentTypeRepository.Delete(id);
        if (createContentTypeResult.IsError)
        {
            return createContentTypeResult.Errors;
        }
        return createContentTypeResult;
    }

    public async Task<ErrorOr<IEnumerable<ContentType>>> GetAll()
    {
        var getContentTypesResult = await _contentTypeRepository.GetAll();
        if (getContentTypesResult.IsError)
        {
            return getContentTypesResult.Errors;
        }
        return getContentTypesResult.Value.ToList();
    }

    public async Task<ErrorOr<ContentType>> GetById(Guid id)
    {
        var getContentTypeResult = await _contentTypeRepository.GetById(id);
        if (getContentTypeResult.IsError)
        {
            return getContentTypeResult.Errors;
        }
        return getContentTypeResult;
    }

    public async Task<ErrorOr<ContentType>> GetByTitle(string title)
    {
        var getContentTypeResult = await _contentTypeRepository.GetByTitle(title);
        if (getContentTypeResult.IsError)
        {
            return getContentTypeResult.Errors;
        }
        return getContentTypeResult;
    }

    public async Task<ErrorOr<ContentType>> Update(ContentType articleTypeUpdate)
    {
        var updateContentTypeResult = await _contentTypeRepository.Update(articleTypeUpdate);
        if (updateContentTypeResult.IsError)
        {
            return updateContentTypeResult.Errors;
        }
        return updateContentTypeResult;
    }
}
