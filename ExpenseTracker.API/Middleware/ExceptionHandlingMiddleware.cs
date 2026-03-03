using System.Text.Json;
using ExpenseTracker.Application.Common.Exceptions;

namespace ExpenseTracker.API.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        logger.LogError(ex, "Exception occured: {Message}.", ex.Message);

        if (httpContext.Response.HasStarted)
        {
            logger.LogWarning("The response has already started, skipping exception middleware execution.");
            return;
        }

        httpContext.Response.ContentType = "application/json";

        var (statusCode, error) = ex switch
        {
            NotFoundException => (404, ex.Message),
            ValidationException => (400, ex.Message),
            ConflictException => (409, ex.Message),
            ForbiddenException => (403, "Access denied."),
            UnauthorizedAccessException => (401, "Please login."),
            JsonException => (400, "Invalid JSON format."),
            BadHttpRequestException => (400, "Bad request body."),
            ArgumentNullException => (400, "Missing required arguments."),

            _ => (500, "Internal Server Error.")
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(new { statusCode, error });
    }
}