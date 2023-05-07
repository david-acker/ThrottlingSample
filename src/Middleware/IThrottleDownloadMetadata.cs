namespace ThrottlingSample.Middleware;

public interface IThrottleDownloadMetadata
{
    int? MaxBytesPerSecond { get; }
}
