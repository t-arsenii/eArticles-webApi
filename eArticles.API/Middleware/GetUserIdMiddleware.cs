using System.Security.Claims;

namespace eArticles.API.Middleware;

public class GetUserIdMiddleware
{
    private readonly RequestDelegate _next;

    public GetUserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity is not null)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (
                    !Guid.TryParse(
                        context.User.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                            ?.Value,
                        out Guid userId
                    )
                )
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("");
                }
                context.Items["userId"] = userId;
            }
        }
        await _next(context);
    }
}
