
using System.Net;
using System.Text.Json;

namespace FilmoSearchPortal.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{Message}", exception.Message);
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is DomainException domainException)
            {
                context.Response.StatusCode = (int)domainException.HttpStatusCode;
                var response = new
                {
                    Message = domainException.Message,
                    Code = domainException.Code,
                    ExceptionMessage = domainException.InnerException?.Message
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new
                {
                    Message = "Internal Server Error. Please try again later.",
                    ExceptionMessage = exception.Message,
                    InnerExceptionMessage = exception.InnerException?.Message
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}

