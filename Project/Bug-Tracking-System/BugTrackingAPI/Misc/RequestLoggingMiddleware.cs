namespace BugTrackingAPI.Misc;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User?.FindFirst("sub")?.Value ?? "Anonymous";
        var endpoint = context.Request.Path;
        var method = context.Request.Method;
        var timestamp = DateTime.UtcNow;

        _logger.LogInformation("Request: {Method} {Endpoint} by {UserId} at {Time}",
            method, endpoint, userId, timestamp);

        await _next(context);
    }
}
