namespace DownloadThrottling;

/// <summary>
/// Metadata that disables download throttling for a controller or endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class DisableDownloadThrottlingAttribute : Attribute, IDownloadThrottlingMetadata
{
    /// <inheritdoc />
    int? IDownloadThrottlingMetadata.MaxBytesPerSecond => null;
}
