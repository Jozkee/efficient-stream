using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    internal abstract class EfficientStream : Stream
    {
        // seal the methods that DO NOT align with best practices, we will provide an impl. that aligns with efficiency according to the newest paradigms in Stream.
        public sealed override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) { throw null; }
        public sealed override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) { throw null; }
        public sealed override int EndRead(IAsyncResult asyncResult) { throw null; }
        public override void EndWrite(IAsyncResult asyncResult) { throw null; }

        // promote to abstract the methods that DO align with best practices.
        public abstract override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);
        public abstract override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);
    }
}
