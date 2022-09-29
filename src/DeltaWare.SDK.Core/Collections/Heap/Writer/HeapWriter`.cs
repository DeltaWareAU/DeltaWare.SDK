using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Heap.Writer
{
    internal sealed class HeapWriter<T> : IHeapWriter<T>
    {
        private readonly ThreadLocal<IHeapWriter<T>> _threadLocalizedWriter;

        public HeapWriter(ThreadLocal<IHeapWriter<T>> threadLocalizedWriter)
        {
            _threadLocalizedWriter = threadLocalizedWriter;
        }

        public int Length => _threadLocalizedWriter.Value!.Length;

        public void Write(T value)
        {
            _threadLocalizedWriter.Value!.Write(value);
        }

        public void Dispose()
        {
            _threadLocalizedWriter.Value!.Dispose();
        }
    }
}
