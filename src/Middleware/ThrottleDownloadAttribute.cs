namespace ThrottlingSample.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ThrottleDownloadAttribute : Attribute, IThrottleDownloadMetadata
{
    private readonly int _bytes;

    public ThrottleDownloadAttribute(int bytes)
    {
        _bytes = bytes;
    }

    int? IThrottleDownloadMetadata.MaxBytesPerSecond => _bytes;
}
