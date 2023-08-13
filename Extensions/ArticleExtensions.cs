using GamingBlog.API.Data;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingBlog.API.Extensions;

public static class ArticleExtensions
{
    public static ArticleDTO? AsDto(this Article article, List<string> tagNames)
    {
        return new ArticleDTO(
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
