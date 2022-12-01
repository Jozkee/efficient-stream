using Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace stream_redesign_netcore31
{
    internal class MyWrappedEfficientStream : EfficientStream2
    {
        private Stream _baseStream;
        private bool _leaveOpen;

        public MyWrappedEfficientStream(Stream stream, bool leaveOpen) 
        { 
            _baseStream = stream;
            _leaveOpen = leaveOpen;
        }

        public override bool CanRead => _baseStream.CanRead;

        public override bool CanSeek => _baseStream.CanSeek;

        public override bool CanWrite => _baseStream.CanWrite;

        public override long Length => _baseStream.Length;

        public override long Position { get => _baseStream.Position; set => _baseStream.Position = value; }

        public override void Flush() => _baseStream.Flush();

        public override int Read(Span<byte> buffer) => _baseStream.Read(buffer);

        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => _baseStream.ReadAsync(buffer, cancellationToken);

        public override long Seek(long offset, SeekOrigin origin) => _baseStream.Seek(offset, origin);

        public override void SetLength(long value) => _baseStream.SetLength(value);

        public override void Write(ReadOnlySpan<byte> buffer) => _baseStream.Write(buffer);

        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => _baseStream.WriteAsync(buffer, cancellationToken);
    }
}
