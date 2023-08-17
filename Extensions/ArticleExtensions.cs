using GamingBlog.API.Data;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingBlog.API.Extensions;

public static class ArticleExtensions
{
    public static ArticleDto AsDto(this Article article)
    {
        List<string>? tagNames = new();
        tagNames = article.Tags.Select(t => t.Title).ToList();
        if (!tagNames.Any())
        {
            tagNames = null;
        }
        return new ArticleDto(
            article.Id,
            article.Title,
            article.Description,
            article.Content,
            article.Article_type.ToString(),
            article.Published_Date.ToString(),
            article.Img_Url,
            tagNames
        );
    }
}
