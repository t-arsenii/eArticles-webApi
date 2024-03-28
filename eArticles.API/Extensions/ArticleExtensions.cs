using eArticles.API.Data.Dtos;
using eArticles.API.Models;

namespace eArticles.API.Extensions;

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
            article.Id.ToString(),
            article.Title,
            article.Description,
            article.Content,
            article.ArticleType.Title,
            article.Published_Date.ToString(),
            article.Img_Url,
            tagNames,
            User: new UserDto(
                article.User.Id.ToString(),
                article.User.FirstName,
                article.User.LastName,
                article.User.UserName,
                article.User.Email,
                article.User.PhoneNumber
            )
        );
    }
}
