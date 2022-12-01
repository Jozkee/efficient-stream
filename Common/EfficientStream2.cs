using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public abstract class EfficientStream2 : EfficientStream
    {
        // seal the methods that DO NOT align with best practices, we will provide an impl. that aligns with efficiency according to the newest paradigms in Stream.
        public sealed override int Read(byte[] buffer, int offset, int count) { throw null; }
        public sealed override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) { throw null; }
        public sealed override void Write(byte[] buffer, int offset, int count) { throw null; }
        public sealed override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) { throw null; }

        // promote to abstract the methods that DO align with best practices.
        public abstract override int Read(Span<byte> buffer);
        public abstract override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default);
        public abstract override void Write(ReadOnlySpan<byte> buffer);
        public abstract override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
    }
}
