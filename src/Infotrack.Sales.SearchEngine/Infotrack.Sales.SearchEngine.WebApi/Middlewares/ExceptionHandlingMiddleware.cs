using Infotrack.Sales.SearchEngine.Contracts;
using Infotrack.Sales.SearchEngine.Domain;
using System.Net;

namespace Infotrack.Sales.SearchEngine.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                LogException(ex, context.Request);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void LogException(Exception exception, HttpRequest request)
        {
            _logger.LogError(
                exception,
                "[Infotrack.Sales.SearchEngine] Encountered exception: {ExceptionType} for request {@Request}",
                exception.GetType().Name,
                request);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = exception switch
            {
                ProviderSearchException ex => new ExceptionResponse { StatusCode = HttpStatusCode.InternalServerError, Description = ex.Message },
                ProviderNotSupportedException ex => new ExceptionResponse { StatusCode = HttpStatusCode.InternalServerError, Description = ex.Message },
                _ => new ExceptionResponse { StatusCode = HttpStatusCode.InternalServerError, Description = "Internal server error. Please retry later." }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
