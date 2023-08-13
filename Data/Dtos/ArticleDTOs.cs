namespace GamingBlog.API.Data.Dtos;

public record ArticleDTO(
    int Id,
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string Published_Date,
    string Img_Url,
    List<string> ArticleTags
);

public record CreateArticleDto(
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string Img_Url,
    List<string> ArticleTags
);

public record UpdateArticleDto(
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string Img_Url,
    List<string> ArticleTags
);