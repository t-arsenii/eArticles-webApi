using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;
namespace eArticles.API.Persistance;
public interface ITagsRepository
{
    Task<Tag?> Create(Tag tag);
    Task<Tag?> Update(Tag tag);
    Task<Tag?> Delete(int id);
    Task<Tag?> GetById(int id);
    Task<IEnumerable<Tag>?> GetAll();
}
