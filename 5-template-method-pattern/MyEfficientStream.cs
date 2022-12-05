namespace _5_template_method_pattern
{
    public class MyEfficientStream : EfficientStreamTmp
    {
        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        protected override int ReadCore(Span<byte> buffer)
        {
            throw new NotImplementedException();
        }

        protected override ValueTask<int> ReadCoreAsync(Memory<byte> buffer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override long SeekCore(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        protected override void SetLengthCore(long value)
        {
            throw new NotImplementedException();
        }

        protected override void WriteCore(ReadOnlySpan<byte> buffer)
        {
            throw new NotImplementedException();
        }

        protected override ValueTask WriteCoreAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}