using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Heap.Reader
{
    internal sealed class HeapReader<T> : IHeapReader<T>
    {
        private readonly ThreadLocal<IHeapReader<T>> _threadLocalizedWriter;

        public HeapReader(ThreadLocal<IHeapReader<T>> threadLocalizedWriter)
        {
            _threadLocalizedWriter = threadLocalizedWriter;
        }

        public int Length => _threadLocalizedWriter.Value!.Length;

        public int Position => _threadLocalizedWriter.Value!.Position;

        public bool TryRead(out T? value)
        {
            return _threadLocalizedWriter.Value!.TryRead(out value);
        }

        public void Dispose()
        {
            _threadLocalizedWriter.Value!.Dispose();
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (_threadLocalizedWriter.Value!.TryRead(out T value))
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
