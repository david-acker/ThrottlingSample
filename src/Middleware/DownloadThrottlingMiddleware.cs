namespace ThrottlingSample.Middleware;

public sealed class DownloadThrottlingMiddleware
{
    private readonly RequestDelegate _next;

    public DownloadThrottlingMiddleware(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);

        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var throttleDownloadMetadata = 
            context.GetEndpoint()?.Metadata?.GetMetadata<IThrottleDownloadMetadata>();

        if (throttleDownloadMetadata?.MaxBytesPerSecond is not int maxBytesPerSecond)
        {
            return _next(context);
        }

        return InvokeCore(context, maxBytesPerSecond);
    }

    private async Task InvokeCore(HttpContext context, int maxBytesPerSecond)
    {
        var originalBody = context.Response.Body;

        ThrottledStream? throttledBody = null;

        try
        {
            throttledBody = new ThrottledStream(originalBody, maxBytesPerSecond);
            context.Response.Body = throttledBody;

            await _next(context);
        }
        finally
        {
            context.Request.Body = originalBody;

            if (throttledBody is not null)
            {
                await throttledBody.DisposeAsync();
            }
        }
    }
}
