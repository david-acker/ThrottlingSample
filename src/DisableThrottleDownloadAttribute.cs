namespace DownloadThrottling;

/// <summary>
/// Metadata that disables download throttling for an endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class DisableDownloadThrottlingAttribute : Attribute, IDownloadThrottlingMetadata
{
    /// <inheritdoc />
    int? IDownloadThrottlingMetadata.MaxBytesPerSecond => null;
}
