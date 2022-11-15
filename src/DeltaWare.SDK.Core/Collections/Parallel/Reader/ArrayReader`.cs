using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Parallel.Reader
{
    [DebuggerDisplay("Readers Allocated {_threadLocalizedWriter.Values.Count}")]
    internal sealed class ArrayReader<T> : IArrayReader<T>
    {
        private readonly ThreadLocal<IArrayReader<T>> _threadLocalizedWriter;

        public ArrayReader(ThreadLocal<IArrayReader<T>> threadLocalizedWriter)
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
            foreach (IArrayReader<T> arrayReader in _threadLocalizedWriter.Values)
            {
                arrayReader.Dispose();
            }
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
