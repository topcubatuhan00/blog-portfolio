namespace Blog.Configurations;

public class AuthorizationRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Eğer kullanıcı authenticate değilse ve /, /login gibi sayfalarda değilse ana sayfaya yönlendir
        var path = context.Request.Path.Value?.ToLower();

        if (!context.User.Identity.IsAuthenticated &&
            path != "/" &&
            !path.StartsWith("/auth/login") &&
            !path.StartsWith("/css") &&
            !path.StartsWith("/js") &&
            !path.StartsWith("/images"))
        {
            context.Response.Redirect("/");
            return;
        }

        await _next(context);
    }
}