using System.Globalization;

namespace Middlewares;

public class BrowserMiddleware
{
    private readonly RequestDelegate _next;

    public BrowserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userAgent = context.Request.Headers["User-Agent"].ToString();
        if (userAgent.Contains("MSIE") || userAgent.Contains("Trident") || userAgent.Contains("Edg"))
        {
            context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/");
            return;
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseBrowserMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BrowserMiddleware>();
    }
}