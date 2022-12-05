namespace _5_template_method_pattern
{
    public abstract class EfficientStreamTmp : Stream
    {
        bool _disposed = false;

        public sealed override int Read(byte[] buffer, int offset, int count)
        {
            ValidateBufferArguments(buffer, offset, count);
            return Read(buffer.AsSpan(offset, count));
        }

        public sealed override int Read(Span<byte> buffer)
        {
            ValidateCanRead();
            ValidateIsDisposed();

            return ReadCore(buffer);
        }

        protected abstract int ReadCore(Span<byte> buffer);

        public sealed override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ValidateBufferArguments(buffer, offset, count);
            return ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
        }

        public sealed override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            ValidateCanRead();
            ValidateIsDisposed();

            return base.ReadAsync(buffer, cancellationToken);
        }

        // Notes:
        // CT default value removed.
        // `*CoreAsync` has 30 results in runtime, none is public API, `*AsyncCore` has zero.
        protected abstract ValueTask<int> ReadCoreAsync(Memory<byte> buffer, CancellationToken cancellationToken);

        public sealed override void Write(byte[] buffer, int offset, int count)
        {
            ValidateBufferArguments(buffer, offset, count);
            Read(buffer.AsSpan(offset, count));
        }

        public sealed override void Write(ReadOnlySpan<byte> buffer)
        {
            ValidateCanWrite();
            ValidateIsDisposed();

            WriteCore(buffer);
        }

        protected abstract void WriteCore(ReadOnlySpan<byte> buffer);

        public sealed override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ValidateBufferArguments(buffer, offset, count);
            return WriteAsync(buffer.AsMemory(offset, count)).AsTask();
        }

        public sealed override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            ValidateCanWrite();
            ValidateIsDisposed();

            return WriteCoreAsync(buffer, cancellationToken);
        }

        protected abstract ValueTask WriteCoreAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken);

        // Note: IsClosed is an implementatio detail.
        // RE: we can track _disposed and expect everyone will call it. It is an anti-pattern to not cascade to the base method.

        // Note: unsealed?
        public override long Seek(long offset, SeekOrigin origin)
        {
            ValidateIsDisposed();
            ValidateCanSeek();

            long pos = origin switch
            {
                SeekOrigin.Begin => offset,
                SeekOrigin.Current => Position + offset,
                SeekOrigin.End => Length + offset,
                _ => throw new ArgumentException("Invalid seek origin", nameof(origin))

            };

            if (pos < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // What does it mean that offset result in a position larger than Length?
            // RE: docs say is supported https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.seek?view=net-6.0
            // > Seeking to any location beyond the length of the stream is supported.

            return SeekCore(offset, origin);
        }

        protected abstract long SeekCore(long offset, SeekOrigin origin);

        public override void SetLength(long value)
        {
            ValidateIsDisposed();
            ValidateCanSeek();
            ValidateCanWrite();

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            SetLengthCore(value);
        }

        protected abstract void SetLengthCore(long value);

        protected override void Dispose(bool disposing)
        {
            _disposed = true;
            base.Dispose(disposing);
        }

        private object? ValidateCanRead() => !CanRead ? throw new InvalidOperationException("Stream is unreadable") : null;

        private object? ValidateCanWrite() => !CanWrite ? throw new InvalidOperationException("Stream is unwrittable") : null;

        private object? ValidateCanSeek() => !CanSeek ? throw new InvalidOperationException("Stream is unseekable") : null;

        private void ValidateIsDisposed() => ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
