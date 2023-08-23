using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Data.Enums;
using GamingBlog.API.Models;

namespace GamingBlog.API.Extensions;

public static class ArticleDtoExtensions
{
    public static Article AsArticle(this BaseArticleDto articleDTO)
    {
        ArticleType a_type = (ArticleType)Enum.Parse(typeof(ArticleType), articleDTO.Article_Type);
        return new Article()
        {
            Title = articleDTO.Title,
            Description = articleDTO.Description,
            Content = articleDTO.Content,
            Article_type = a_type,
            Img_Url = articleDTO.Img_Url!
        };
    }
    
}
