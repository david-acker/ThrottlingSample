namespace ThrottlingSample.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ThrottleDownloadAttribute : Attribute, IThrottleDownloadMetadata
{
    private readonly int _maxBytesPerSecond;

    public ThrottleDownloadAttribute(int maxBytesPerSecond)
    {
        if (maxBytesPerSecond <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxBytesPerSecond), maxBytesPerSecond, "The maximum bytes per second must be greater than zero.");
        }
           
        _maxBytesPerSecond = maxBytesPerSecond;
    }

    int? IThrottleDownloadMetadata.MaxBytesPerSecond => _maxBytesPerSecond;
}
