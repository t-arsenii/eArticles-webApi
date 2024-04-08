using eArticles.API.Data.Dtos;
using eArticles.API.Data.Enums;
using eArticles.API.Models;

namespace eArticles.API.Extensions;

public static class ArticleDtoExtensions
{
    public static Article AsArticle(this BaseArticleDto articleDTO)
    {
        return new Article()
        {
            Title = articleDTO.Title,
            Description = articleDTO.Description,
            Content = articleDTO.Content,
            Img_Url = articleDTO.ImgUrl!
        };
    }

}
