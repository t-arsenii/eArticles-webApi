using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services.Comments;

public interface ICommentService
{
    Task<ErrorOr<Comment>> Create(Comment comment);
    Task<ErrorOr<IEnumerable<Comment>>> GetArticleComments(Guid id);
    Task<ErrorOr<Comment>> GetById(Guid id);
}
