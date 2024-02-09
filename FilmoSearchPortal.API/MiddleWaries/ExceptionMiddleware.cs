using System.Net;

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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled exception: {ex}");

                // Установим статус-код в Internal Server Error (500)
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                await context.Response.WriteAsync("Internal Server Error. Something went wrong.");
            }
        }
    }
}
