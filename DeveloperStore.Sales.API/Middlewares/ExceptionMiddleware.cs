using Serilog;
using System.Net;
using System.Text.Json;

namespace DeveloperStore.Sales.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {                
                Log.Information("➡️ Request: {method} {url}", context.Request.Method, context.Request.Path);

                await _next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                Log.Error(ex,
                    """
                    ❌ Unhandled Exception
                    ➤ ID: {ErrorId}
                    ➤ Path: {Path}
                    ➤ Method: {Method}
                    ➤ Query: {QueryString}
                    ➤ Message: {Message}
                    """,
                    errorId,
                    context.Request.Path,
                    context.Request.Method,
                    context.Request.QueryString,
                    ex.Message
                );

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    status = 500,
                    message = "An unexpected error occurred.",
                    errorId,
                    path = context.Request.Path,
                    method = context.Request.Method,
                    exception = _env.IsDevelopment() ? ex.Message : null
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
            }
        }
    }
}
