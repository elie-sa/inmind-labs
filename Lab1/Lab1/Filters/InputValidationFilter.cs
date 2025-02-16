using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lab1.Filters;

public class InputValidationFilter: IActionFilter
{
    private readonly ILogger<InputValidationFilter> _logger;

    public InputValidationFilter(ILogger<InputValidationFilter> logger)
    {
        _logger = logger;
    }
    
    // old logger
    /*public void OnActionExecuting(ActionExecutingContext context)
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
    }*/
    
    // checking for validation [Required] [EmailAddress] 
    // users/edit if i provide a non valid email a validation error will occur
    // i disabled .NET's default input validation to be able to view my own filter input validation in program.cs ConfigureApiBehaviorOptions
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Any())
                .ToDictionary(e => e.Key,
                    e => e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
                );

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Input Validation Failed",
                Extensions = { ["errors"] = errors }
            };

            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }

    
    // kept this function as a logger since i wanted to also demonstrate the OnActionExecuted and this filter was the only way to show the response body in the logger
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