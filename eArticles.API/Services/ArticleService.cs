using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services;

public class ArticleService : IArticleService
{
    IArticlesRepository _articlesRepository;
    IContentTypeRepository _contentTypeRepository;
    ICategoryRepository _categoryRepository;
    ITagsRepository _tagsRepository;
    public ArticleService(
        IArticlesRepository articlesRepository,
        IContentTypeRepository contentTypeRepository,
        ICategoryRepository categoryRepository,
        ITagsRepository tagsRepository)
    {
        _articlesRepository = articlesRepository;
        _contentTypeRepository = contentTypeRepository;
        _tagsRepository = tagsRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Article?>> Create(
        Article newArticle,
        string contentType,
        string category,
        IEnumerable<string>? tagNames = null)
    {
        if (tagNames != null && tagNames.Any())
        {
            foreach (var tagName in tagNames)
            {
                var getTagResult = await _tagsRepository.GetByTitle(tagName);
                if (getTagResult.IsError)
                {
                    return getTagResult.Errors;
                }
                newArticle.Tags.Add(getTagResult.Value);
            }
        }
        var getContentTypeResult = await _contentTypeRepository.GetByTitle(contentType);
        var getCategoryResult = await _categoryRepository.GetByTitle(category);
        if (getContentTypeResult.IsError)
        {
            return getContentTypeResult.Errors;
        }
        if (getCategoryResult.IsError)
        {
            return getCategoryResult.Errors;
        }
        newArticle.ContentType = getContentTypeResult.Value;
        newArticle.Category = getCategoryResult.Value;
        newArticle.Published_Date = DateTime.Now;
        return newArticle;
    }

    public Task<ErrorOr<Article>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Article>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<IEnumerable<Article>>> GetPage(int currentPage = 1, int pageSize = 10, int? userId = null, string? contentType = null, string? category = null, string? order = null, string[]? tags = null)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<int>> GetTotalItems(int? userId = null)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Article>> Update(Article updateArticle, string contentType, string category, IEnumerable<string>? tagNames = null)
    {
        throw new NotImplementedException();
    }
}
