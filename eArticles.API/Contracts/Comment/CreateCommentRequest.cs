using System.ComponentModel.DataAnnotations;

public record CreateCommentRequest(
    [Required] string content,
    [Required] Guid articleId,
    Guid? parentCommentId
);