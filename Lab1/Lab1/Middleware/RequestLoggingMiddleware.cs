using System.Text;

namespace Lab1;


public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
    {
        _logger = logger;
    }
    
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;
        var requestTime = DateTime.UtcNow;

        var requestInfo = $"[{requestTime}] HTTP {request.Method} {request.Path}{request.QueryString}";

        var headers = new StringBuilder();
        foreach (var header in request.Headers)
        {
            headers.AppendLine($"{header.Key}: {header.Value}");
        }

        string requestBody = string.Empty;
        if (request.ContentLength > 0 && request.Method != "GET")
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, leaveOpen: true);
            requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
        }

        _logger.LogInformation($"{requestInfo}\nHeaders:\n{headers}\nBody:\n{requestBody}");

        await next(context);

        var responseStatusCode = context.Response.StatusCode;
        _logger.LogInformation($"Response Status: {responseStatusCode}");
    }
}