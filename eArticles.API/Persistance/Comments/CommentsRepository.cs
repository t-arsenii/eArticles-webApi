using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance.Comments;

public class CommentsRepository : ICommentsRepository
{
    eArticlesDbContext _dbContext;

    public CommentsRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Comment>> Create(Comment comment)
    {
        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<ErrorOr<IEnumerable<Comment>>> GetArticleComments(Guid articleId)
    {
        var comments = await _dbContext.Comments.Where(e => e.ArticleId == articleId).ToListAsync();
        if (!comments.Any())
        {
            return Error.NotFound(description: $"Comments are not found for specified article (articleId: {articleId})");
        }
        return comments;
    }

    public async Task<ErrorOr<Comment>> GetById(Guid id)
    {
        var comment = _dbContext.Comments.Find(id);
        if(comment is null){
            return Error.NotFound(description: $"Comment is not found (comment id: {id})");
        }
        return comment;
    }
}
