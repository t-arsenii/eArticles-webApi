using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance.Comments;

public interface ICommentsRepository
{
    Task<ErrorOr<Comment>> Create(Comment comment);
    Task<ErrorOr<IEnumerable<Comment>>> GetArticleComments(Guid articleId);
    Task<ErrorOr<Comment>> GetById(Guid id);
}
