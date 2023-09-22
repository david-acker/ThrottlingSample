namespace ThrottlingSample.Middleware;

public sealed class ThrottledStream : Stream
{
    private readonly Stream _innerStream;

    private long _bytesWritten;
    private int _maxBytesPerSecond;
    private DateTime _lastWriteTime;

    public ThrottledStream(Stream innerStream, int maxBytesPerSecond)
    {
        _innerStream = innerStream;

        _maxBytesPerSecond = maxBytesPerSecond;
        _lastWriteTime = DateTime.UtcNow;
    }

    public override bool CanRead => _innerStream.CanRead;

    public override bool CanSeek => _innerStream.CanSeek;

    public override bool CanWrite => _innerStream.CanWrite;

    public override long Length => _innerStream.Length;

    public override long Position
    {
        get => _innerStream.Position;
        set => _innerStream.Position = value;
    }

    public override void Flush() => _innerStream.Flush();

    public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);

    public override void SetLength(long value) => _innerStream.SetLength(value);

    public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);

    public override void Write(byte[] buffer, int offset, int count)
    {
        int start = offset;
        int remainingBytes = count;

        while (remainingBytes > 0)
        {
            var newBytesWritten = Math.Min(remainingBytes, _maxBytesPerSecond);

            Throttle(newBytesWritten);
            _innerStream.Write(buffer, start, newBytesWritten);

            start += newBytesWritten;
            remainingBytes -= newBytesWritten;
        }
    }

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        int start = 0;
        int remainingBytes = buffer.Length;

        while (remainingBytes > 0)
        {
            var newBytesWritten = Math.Min(remainingBytes, _maxBytesPerSecond);

            Throttle(newBytesWritten);
            _innerStream.Write(buffer.Slice(start, newBytesWritten));

            start += newBytesWritten;
            remainingBytes -= newBytesWritten;
        }
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
{
        int start = offset;
        int remainingBytes = count;

        while (remainingBytes > 0)
        {
            var newBytesWritten = Math.Min(remainingBytes, _maxBytesPerSecond);

            await ThrottleAsync(newBytesWritten, cancellationToken);
            await _innerStream.WriteAsync(buffer, start, newBytesWritten);

            start += newBytesWritten;
            remainingBytes -= newBytesWritten;
        }
    }

    public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        int start = 0;
        int remainingBytes = buffer.Length;

        while (remainingBytes > 0)
        {
            var newBytesWritten = Math.Min(remainingBytes, _maxBytesPerSecond);

            await ThrottleAsync(newBytesWritten, cancellationToken);
            await _innerStream.WriteAsync(buffer.Slice(start, newBytesWritten), cancellationToken);

            start += newBytesWritten;
            remainingBytes -= newBytesWritten;
        }
    }
    
    private async Task ThrottleAsync(int newBytesWritten, CancellationToken cancellationToken = default)
    {
        _bytesWritten += newBytesWritten;

        var elapsed = DateTime.UtcNow - _lastWriteTime;
        var bytesPerSecond = _bytesWritten / elapsed.TotalSeconds;

        if (bytesPerSecond >= _maxBytesPerSecond)
        {
            int delayMilliseconds = (int)(1_000 - elapsed.TotalMilliseconds);
            await Task.Delay(delayMilliseconds, cancellationToken);
        }

        _bytesWritten = 0;
        _lastWriteTime = DateTime.UtcNow;
    }

    private void Throttle(int newBytesWritten)
    {
        _bytesWritten += newBytesWritten;

        var elapsed = DateTime.UtcNow - _lastWriteTime;
        var bytesPerSecond = _bytesWritten / elapsed.TotalSeconds;

        if (bytesPerSecond >= _maxBytesPerSecond)
        {
            int delayMilliseconds = (int)(1_000 - elapsed.TotalMilliseconds);
            Thread.Sleep(delayMilliseconds);
        }

        _bytesWritten = 0;
        _lastWriteTime = DateTime.UtcNow;
    }
}
