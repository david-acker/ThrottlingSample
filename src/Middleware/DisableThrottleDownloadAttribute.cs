namespace ThrottlingSample.Middleware;

/// <summary>
/// Disables the maximum allowed download speed.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class DisableThrottleDownloadAttribute : Attribute, IThrottleDownloadMetadata
{
    /// <inheritdoc />
    int? IThrottleDownloadMetadata.MaxBytesPerSecond => null;
}
