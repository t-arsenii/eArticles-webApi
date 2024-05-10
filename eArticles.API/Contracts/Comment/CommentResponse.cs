using System.ComponentModel.DataAnnotations;

public record CommentResponse(
    Guid id,
    string content,
    Guid articleId,
    Guid? parentCommentId,
    DateTime published_Date,
    Guid userId
);
