using Entities.ErrorModel;

namespace Watan.Extensions;

public class ExpiredOrMissedTokenMiddleware
{
    private readonly RequestDelegate _next;

    public ExpiredOrMissedTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value != null)
        {
            var path = context.Request.Path.Value.ToLower();
            if (!path.Contains("login") && !path.Contains("refresh_token")&& !path.Contains("hierarchy"))
            {
                if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "The provided token is expired or missing !"
                    }.ToString());
                    return;
                }
            }
        }
        
        await _next(context);
    }
}