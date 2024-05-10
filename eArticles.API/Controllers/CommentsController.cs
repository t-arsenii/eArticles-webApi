using System.Security.Claims;
using eArticles.API.Models;
using eArticles.API.Services.Comments;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest commentRequest)
    {
        Guid userId;
        if (
            !Guid.TryParse(
                User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out userId
            )
        )
        {
            return BadRequest("Wrong user id format");
        }
        ;
        var comment = new Comment()
        {
            Content = commentRequest.content,
            ArticleId = commentRequest.articleId,
            ParentCommentId = commentRequest.parentCommentId,
            UserId = userId
        };
        var createCommentResult = await _commentService.Create(comment);
        if (createCommentResult.IsError)
        {
            return BadRequest(createCommentResult.FirstError.Description);
        }
        var createdComment = createCommentResult.Value;
        return Ok(
            new CommentResponse(
                id: createdComment.Id,
                content: createdComment.Content,
                articleId: createdComment.ArticleId,
                parentCommentId: createdComment.ParentCommentId,
                published_Date: createdComment.Published_Date,
                userId: createdComment.UserId
            )
        );
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetArticleComments([FromRoute(Name = "id")] Guid articleId) {
        var getCommentsResult = await _commentService.GetArticleComments(articleId);
        if(getCommentsResult.IsError){
            return NotFound(getCommentsResult.FirstError.Description);
        }
        var commentsResponse = new List<CommentResponse>();
        foreach(var comment in getCommentsResult.Value){
            commentsResponse.Add(new CommentResponse(
                id: comment.Id,
                content: comment.Content,
                articleId: comment.ArticleId,
                parentCommentId: comment.ParentCommentId,
                published_Date: comment.Published_Date,
                userId: comment.UserId));
        }
        return Ok(commentsResponse);
     }
}
