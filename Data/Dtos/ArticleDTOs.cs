namespace GamingBlog.API.Data.Dtos;

public record ArticleDto(
    int Id,
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string Published_Date,
    string Img_Url,
    List<string>? ArticleTags
);

public abstract record BaseArticleDto(string Title,
    string Description,
    string Content,
    string Article_Type,
    string? Img_Url,
    List<string>? ArticleTags);

public record CreateArticleDto(
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string? Img_Url,
    List<string>? ArticleTags
) : BaseArticleDto(Title, Description, Content, Article_Type, Img_Url, ArticleTags);

public record UpdateArticleDto(
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string Img_Url,
    List<string>? ArticleTags
): BaseArticleDto(Title, Description, Content, Article_Type, Img_Url, ArticleTags);