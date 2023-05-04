using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestEditing
    {
        private readonly RequestDelegate _next;

        public RequestEditing(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["IsChromeBrowser"] = httpContext.Request.Headers["User-Agent"].Any(
                p => p.ToLower().Contains("chrome"));
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestEditingExtensions
    {
        public static IApplicationBuilder UseRequestEditing(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestEditing>();
        }
    }
}
