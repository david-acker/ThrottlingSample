namespace ThrottlingSample.Middleware;

/// <summary>
/// Interface marking attributes that specify the maximum allowed download speed.
/// </summary>
public interface IThrottleDownloadMetadata
{
    /// <summary>
    /// The maximum allowed download speed in bytes per second.
    /// </summary>
    int? MaxBytesPerSecond { get; }
}
