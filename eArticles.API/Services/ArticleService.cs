using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services;

public class ArticleService : IArticleService
{
    IArticlesRepository _articlesRepository;
    IContentTypeService _contentTypeRepository;
    ICategoriesRepository _categoryRepository;
    ITagsRepository _tagsRepository;
    public ArticleService(
        IArticlesRepository articlesRepository,
        IContentTypeService contentTypeRepository,
        ICategoriesRepository categoryRepository,
        ITagsRepository tagsRepository)
    {
        _articlesRepository = articlesRepository;
        _contentTypeRepository = contentTypeRepository;
        _tagsRepository = tagsRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Article>> Create(
        Article newArticle,
        IEnumerable<string>? tagIds = null)
    {
        if (tagIds != null && tagIds.Any())
        {
            foreach (var tagId in tagIds)
            {
                int id = int.Parse(tagId);
                var getTagResult = await _tagsRepository.GetById(id);
                if (getTagResult.IsError)
                {
                    return getTagResult.Errors;
                }
                newArticle.Tags.Add(getTagResult.Value);
            }
        }
        var getContentTypeResult = await _contentTypeRepository.GetById(newArticle.ContentTypeId);
        var getCategoryResult = await _categoryRepository.GetById(newArticle.CategoryId);
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
        var createArticleResult = await _articlesRepository.Create(newArticle);
        if (createArticleResult.IsError)
        {
            return createArticleResult.Errors;
        }
        return createArticleResult.Value;
    }

    public async Task<ErrorOr<Article>> Delete(int id)
    {
        var deleteArticleResult = await _articlesRepository.Delete(id);
        if (deleteArticleResult.IsError)
        {
            return deleteArticleResult.Errors;
        }
        return deleteArticleResult.Value;
    }

    public async Task<ErrorOr<Article>> GetById(int id)
    {
        var getArticleResult = await _articlesRepository.GetById(id);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        return getArticleResult.Value;
    }

    public async Task<ErrorOr<IEnumerable<Article>>> GetPage(
        int currentPage = 1,
        int pageSize = 10,
        int? userId = null,
        string? contentType = null,
        string? category = null,
        string? order = null,
        string[]? tags = null)
    {
        var getArticlePageResult = await _articlesRepository.GetPage(currentPage, pageSize, userId, contentType, category, order, tags);
        if (getArticlePageResult.IsError)
        {
            return getArticlePageResult.Errors;
        }
        return getArticlePageResult.Value.ToList();
    }

    public async Task<ErrorOr<int>> GetTotalItems(int? userId = null)
    {
        var getTotalItemsResult = await _articlesRepository.GetTotalItems(userId);
        if (getTotalItemsResult.IsError)
        {
            return getTotalItemsResult.Errors;
        }
        return getTotalItemsResult.Value;
    }

    public async Task<ErrorOr<Article>> Update(Article updateArticle, IEnumerable<string>? tagIds = null)
    {
        var getArticleResult = await _articlesRepository.GetById(updateArticle.Id);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        if (tagIds != null && tagIds.Any())
        {
            foreach (var tagName in tagIds)
            {
                var getTagResult = await _tagsRepository.GetByTitle(tagName);
                if (getTagResult.IsError)
                {
                    return getArticleResult.Errors;
                }
            }
        }
        var getContentTypeResult = await _contentTypeRepository.GetById(updateArticle.ContentTypeId);
        var getCategoryResult = await _categoryRepository.GetById(updateArticle.CategoryId);
        if (getContentTypeResult.IsError)
        {
            return getContentTypeResult.Errors;
        }
        if (getCategoryResult.IsError)
        {
            return getCategoryResult.Errors;
        }
        var updateArticleResult = await _articlesRepository.Update(updateArticle);
        if (updateArticleResult.IsError)
        {
            return updateArticleResult.Errors;
        }
        return updateArticleResult.Value;
    }
}
