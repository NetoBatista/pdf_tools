using System.Diagnostics;

namespace PdfTools.Extension;

public class MiddlewareExtension
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareExtension> _logger;

    public MiddlewareExtension(RequestDelegate next, ILogger<MiddlewareExtension> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _next(httpContext);

            stopwatch.Stop();
            var timeExecution = (int)stopwatch.Elapsed.TotalMilliseconds;

            _logger.LogInformation($"Executing {httpContext.Request.Path} {httpContext.Request.Method} - {DateTime.Now} - Time {timeExecution}ms - StatusCode {httpContext.Response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error executing {httpContext.Request.Path} - {DateTime.Now} - {ex}");
        }
    }
}
