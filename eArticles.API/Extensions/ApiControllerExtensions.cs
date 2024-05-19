using eArticles.API.Controllers;

public static class ApiControllerExtensions
{
    public static Guid GetLoggedUserId(this ApiController controller)
    {
        if (
            controller.HttpContext.Items.TryGetValue("userId", out var userIdObj)
            && userIdObj is Guid userIdGuid
        )
        {
            return userIdGuid;
        }
        ;
        throw new ArgumentException("Error getting user id from HttpContext items");
    }
}
