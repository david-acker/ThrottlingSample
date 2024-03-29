﻿using Microsoft.AspNetCore.Builder;

namespace DownloadThrottling;

/// <summary>
/// Extension methods for the download throttling middleware.
/// </summary>
public static class DownloadThrottlingBuilderExtensions
{
    /// <summary>
    /// Adds middleware for throttling download speeds.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder"/> instance this method extends.</param>
    public static IApplicationBuilder UseDownloadThrottling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DownloadThrottlingMiddleware>();
    }
}
