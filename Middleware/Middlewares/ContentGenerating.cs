using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ContentGenerating
    {
        private readonly RequestDelegate _next;

        public ContentGenerating(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower().Contains("content"))
            {
                await httpContext.Response.WriteAsync("This message from .....");
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ContentGeneratingExtensions
    {
        public static IApplicationBuilder UseContentGenerating(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ContentGenerating>();
        }
    }
}
