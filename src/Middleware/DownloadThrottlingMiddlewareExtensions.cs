namespace ThrottlingSample.Middleware;

public static class DownloadThrottlingMiddlewareExtensions
{
    public static IApplicationBuilder UseDownloadThrottling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DownloadThrottlingMiddleware>();
    }
}
