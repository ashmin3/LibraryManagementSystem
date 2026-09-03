using System.Text.Json;

namespace LibraryManagementSystem.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                context.Response.ContentType = "application/json";
                _logger.LogError(ex.Message, "Something went Wrong");

                if(ex is KeyNotFoundException)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    var response = new
                    {
                        Message = ex.Message
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
                else if (ex is InvalidOperationException)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    var response = new
                    {
                        Message = ex.Message
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
                else if (ex is ArgumentException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var response = new
                    {
                        Message = ex.Message
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
                else if(ex is UnauthorizedAccessException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    var response = new
                    {
                        Message = ex.Message
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var response = new
                    {
                        Message = "Something Went Wrong"

                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            }
        }
    }
}
