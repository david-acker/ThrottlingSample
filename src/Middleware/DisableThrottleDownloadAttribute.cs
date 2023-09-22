namespace ThrottlingSample.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class DisableThrottleDownloadAttribute : Attribute, IThrottleDownloadMetadata
{
    int? IThrottleDownloadMetadata.MaxBytesPerSecond => null;
}
