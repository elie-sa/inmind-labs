using Microsoft.AspNetCore.Diagnostics;

namespace Lab1.Middleware;

public class ExceptionHandlingMiddleware: IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        
        var statusCode = exception switch
        {
            ArgumentException or ArgumentNullException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            BadHttpRequestException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var response = new
        {
            StatusCode = statusCode, exception.Message
        };
        
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return ValueTask.FromResult(true);
    }
}