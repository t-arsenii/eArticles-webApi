using Azure;
using eArticles.API.Models;
using eArticles.API.Persistance.Tags;
using ErrorOr;

namespace eArticles.API.Services.Tags;

public class TagsService : ITagsService
{
    ITagsRepository _tagsRepository;
    public TagsService(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async Task<ErrorOr<Tag>> Create(Tag tag)
    {
        var createTagResult = await _tagsRepository.Create(tag);
        if (createTagResult.IsError)
        {
            return createTagResult.Errors;
        }
        return createTagResult.Value;
    }

    public async Task<ErrorOr<Tag>> Delete(Guid id)
    {
        var deleteTagResult = await _tagsRepository.Delete(id);
        if (deleteTagResult.IsError)
        {
            return deleteTagResult.Errors;
        }
        return deleteTagResult.Value;
    }

    public async Task<ErrorOr<IEnumerable<Tag>>> GetAll()
    {
        var getTagsResult = await _tagsRepository.GetAll();
        if (getTagsResult.IsError)
        {
            return getTagsResult.Errors;
        }
        return getTagsResult.Value.ToList();
    }

    public async Task<ErrorOr<Tag>> GetById(Guid id)
    {
        var getTagResult = await _tagsRepository.GetById(id);
        if (getTagResult.IsError)
        {
            return getTagResult.Errors;
        }
        return getTagResult.Value;
    }

    public async Task<ErrorOr<Tag>> GetByTitle(string title)
    {
        var getTagResult = await _tagsRepository.GetByTitle(title);
        if (getTagResult.IsError)
        {
            return getTagResult.Errors;
        }
        return getTagResult.Value;
    }

    public async Task<ErrorOr<Tag>> Update(Tag tag)
    {
        var getTagResult = await _tagsRepository.GetById(tag.Id);
        if (getTagResult.IsError)
        {
            return getTagResult.Errors;
        }
        var updateTagResult = await _tagsRepository.Update(tag);
        if (updateTagResult.IsError)
        {
            return updateTagResult.Errors;
        }
        return updateTagResult.Value;
    }
}
