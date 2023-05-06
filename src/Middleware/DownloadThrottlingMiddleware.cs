namespace ThrottlingSample.Middleware;

public sealed class DownloadThrottlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _maxBytesPerSecond = 100_000;

    public DownloadThrottlingMiddleware(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);

        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var originalBody = context.Response.Body;
        var throttledBody = new ThrottledStream(originalBody, _maxBytesPerSecond);

        try
        {
            context.Response.Body = throttledBody;
            await _next(context);
        }
        finally
        {
            context.Request.Body = originalBody;
            await throttledBody.DisposeAsync();
        }
    }
}
