namespace ThrottlingSample.Middleware;

/// <summary>
/// Sets the maximum allowed download speed to the specified bytes per second.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ThrottleDownloadAttribute : Attribute, IThrottleDownloadMetadata
{
    private readonly int _bytesPerSecond;

    /// <summary>
    /// Creates a new instance of <see cref="ThrottleDownloadAttribute"/>.
    /// </summary>
    /// <param name="bytesPerSecond">The maximum download speed.</param>
    public ThrottleDownloadAttribute(int bytesPerSecond)
    {
        if (bytesPerSecond <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bytesPerSecond), bytesPerSecond, "The maximum download speed must be greater than zero.");
        }
           
        _bytesPerSecond = bytesPerSecond;
    }

    /// <inheritdoc />
    int? IThrottleDownloadMetadata.MaxBytesPerSecond => _bytesPerSecond;
}
