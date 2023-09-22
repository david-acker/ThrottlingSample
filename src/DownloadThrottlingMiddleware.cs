using Microsoft.AspNetCore.Http;

namespace DownloadThrottling;

/// <summary>
/// Enables download throttling for requests.
/// </summary>
internal sealed class DownloadThrottlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes the download throttling middleware.
    /// </summary>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    public DownloadThrottlingMiddleware(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);

        _next = next;
    }

    /// <summary>
    /// Invoke the middleware.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <returns>A task that represents the execution of this middleware.</returns>
    public Task Invoke(HttpContext context)
    {
        var downloadThrottlingMetadata = 
            context.GetEndpoint()?.Metadata?.GetMetadata<IDownloadThrottlingMetadata>();
        
        return downloadThrottlingMetadata?.MaxBytesPerSecond is not { } maxBytesPerSecond 
            ? _next(context) 
            : InvokeCore(context, maxBytesPerSecond);
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
