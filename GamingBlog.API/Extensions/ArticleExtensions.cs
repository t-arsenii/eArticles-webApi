using GamingBlog.API.Data;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Data.Enums;
using GamingBlog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingBlog.API.Extensions;

public static class ArticleExtensions
{
    public static ArticleDto AsDto(this Article article)
    {
        List<string>? tagNames = article.Tags.Select(t => t.Title).ToList();
        if (!tagNames.Any())
        {
            tagNames = null;
        }
        return new ArticleDto(
            article.Id.ToString(),
            article.Title,
            article.Description,
            article.Content,
            article.Article_type.ToString(),
            article.Published_Date.ToString(),
            article.Img_Url,
            tagNames,
            article.UserId.ToString()!
        );
    }

    public static void UpdateFromDto(this Article article, UpdateArticleDto updateArticleDto)
    {
        var tagNames = updateArticleDto.ArticleTags;
        if (tagNames != null && tagNames.Any())
        {
            foreach (var tagName in tagNames)
            {
                var tag = _dbContext.Tags.FirstOrDefault(t => t.Title == tagName);
                if (tag == null)
                {
                    tag = new Tag { Title = tagName };
                    _dbContext.Tags.Add(tag);
                }
                newArticle.Tags.Add(tag);
            }
        }
        ArticleType articleType = (ArticleType)Enum.Parse(typeof(ArticleType), updateArticleDto.ArticleType);
        
            article.Title = updateArticleDto.Title;
            article.Description = updateArticleDto.Description;
            article.Content = updateArticleDto.Content;
            article.Article_type = articleType;
           article.Published_Date.ToString();
            article.Img_Url = updateArticleDto.Img_Url;
            article.Tags =  
            article.UserId.ToString()!
    }
}
