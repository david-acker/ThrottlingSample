namespace DownloadThrottling;

/// <summary>
/// Metadata that enables download throttling for a controller or endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class EnableDownloadThrottlingAttribute : Attribute, IDownloadThrottlingMetadata
{
    private readonly int _bytesPerSecond;

    /// <summary>
    /// Creates a new instance of <see cref="EnableDownloadThrottlingAttribute"/>.
    /// </summary>
    /// <param name="bytesPerSecond">The maximum download speed.</param>
    public EnableDownloadThrottlingAttribute(int bytesPerSecond)
    {
        if (bytesPerSecond <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bytesPerSecond), bytesPerSecond, "The maximum download speed must be greater than zero.");
        }
           
        _bytesPerSecond = bytesPerSecond;
    }

    /// <inheritdoc />
    int? IDownloadThrottlingMetadata.MaxBytesPerSecond => _bytesPerSecond;
}
