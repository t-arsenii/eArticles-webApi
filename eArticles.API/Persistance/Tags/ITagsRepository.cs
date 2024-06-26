﻿using eArticles.API.Contracts.User;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
namespace eArticles.API.Persistance.Tags;
public interface ITagsRepository
{
    Task<ErrorOr<Tag>> Create(Tag tag);
    Task<ErrorOr<Tag>> Update(Tag tag);
    Task<ErrorOr<Tag>> Delete(Guid id);
    Task<ErrorOr<Tag>> GetById(Guid id);
    Task<ErrorOr<Tag>> GetByTitle(string title);
    Task<ErrorOr<IEnumerable<Tag>>> GetAll();
}
