using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResponseEditing
    {
        private readonly RequestDelegate _next;

        public ResponseEditing(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode==404)
            {
                httpContext.Response.WriteAsync("Not Found");
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResponseEditingExtensions
    {
        public static IApplicationBuilder UseResponseEditing(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseEditing>();
        }
    }
}
