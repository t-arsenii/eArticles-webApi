using eArticles.API.Contracts.Article;
using eArticles.API.Contracts.User;
using eArticles.API.Models;

namespace eArticles.API.Extensions;

public static class ArticleExtensions
{
    public static ArticleResponse AsDto(this Article article)
    {
        List<string>? tagNames = article.Tags?.Select(t => t.Title).ToList();
        string? imageName = article.ImagePath?.Split(@"\").Last();
        return new ArticleResponse(
             Id: article.Id,
             Title: article.Title,
             Description: article.Description,
             Content: article.Content,
             ContentType: article.ContentType.Title,
             Category: article.Category.Title,
             PublishedDate: article.Published_Date.ToString(),
             ImgName: imageName,
             AverageRating: article.AverageRating,
             Tags: tagNames,
             User: new UserResponse(
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
