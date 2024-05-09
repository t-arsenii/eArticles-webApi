using eArticles.API.Models;
using eArticles.API.Persistance.Articles;
using eArticles.API.Persistance.Comments;
using ErrorOr;

namespace eArticles.API.Services.Comments;

public class CommentService : ICommentService
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IArticlesRepository _articlesRepository;

    public CommentService(
        ICommentsRepository commentsRepository,
        IArticlesRepository articlesRepository
    )
    {
        _commentsRepository = commentsRepository;
        _articlesRepository = articlesRepository;
    }

    public async Task<ErrorOr<Comment>> Create(Comment comment)
    {
        var getArticleResult = await _articlesRepository.GetById(comment.ArticleId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }

        if (comment.ParentCommentId.HasValue)
        {
            var getParrentCommentResult = await _commentsRepository.GetById(
                comment.ParentCommentId.Value
            );
            if (getParrentCommentResult.IsError)
            {
                return getParrentCommentResult.Errors;
            }
        }
        
        var createCommentResult = await _commentsRepository.Create(comment);
        if (createCommentResult.IsError)
        {
            return createCommentResult.Errors;
        }
        return createCommentResult.Value;
    }

    public async Task<ErrorOr<IEnumerable<Comment>>> GetArticleComments(Guid articleId)
    {
        var getArticleResult = await _articlesRepository.GetById(articleId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        var createCommentResult = await _commentsRepository.GetArticleComments(articleId);
        if (createCommentResult.IsError)
        {
            return createCommentResult.Errors;
        }
        return createCommentResult.Value.ToList();
    }

    public async Task<ErrorOr<Comment>> GetById(Guid id)
    {
        var getCommentResult = await _commentsRepository.GetById(id);
        if (getCommentResult.IsError)
        {
            return getCommentResult.Errors;
        }
        return getCommentResult.Value;
    }
}
