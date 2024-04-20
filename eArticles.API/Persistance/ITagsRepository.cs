using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
namespace eArticles.API.Persistance;
public interface ITagsRepository
{
    Task<ErrorOr<Tag>> Create(Tag tag);
    Task<ErrorOr<Tag>> Update(Tag tag);
    Task<ErrorOr<Tag>> Delete(int id);
    Task<ErrorOr<Tag>> GetById(int id);
    Task<ErrorOr<Tag>> GetByTitle(string name);
    Task<ErrorOr<IEnumerable<Tag>>> GetAll();
}
