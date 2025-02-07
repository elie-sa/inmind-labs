using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lab1.Filters;

public class LoggingActionFilter: IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // using customDate to show the ms as well (since execution time would be very small)
        var customDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        // I'm aware that UtcNow would be better if the server is remote but since I'm only running it locally I'd like to see the time with respect to Lebanese time
        _logger.LogInformation($"Starting'{context.ActionDescriptor.DisplayName}' at: {customDate}");

        _logger.LogInformation("Parameters:");
        foreach (var param in context.ActionArguments)
        {
            _logger.LogInformation($" {param.Key} = {param.Value}");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var customDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' ended at: {customDate}");

        var result = context.Result;
        _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
        
        if (result is OkObjectResult okResult)
        {
            var value = okResult.Value;
        
            // I'm usually only returning a body in the 200 OkObject, the errors are handled in the ExceptionHandlingMiddleware 
            // and logged using RequestLoggingMiddleware since they won't reach this point since filters are evaluated after all middlewares
            // Couldn't seek the HttpContext wasn't seekable by default so I used a JsonSerializer
            _logger.LogInformation("Response Body: " + JsonSerializer.Serialize(value));
        }

    }  
}