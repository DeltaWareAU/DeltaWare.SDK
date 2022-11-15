using System.Diagnostics;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Parallel.Writer
{
    [DebuggerDisplay("Writers Allocated {_threadLocalizedWriter.Values.Count}")]
    internal sealed class ArrayWriter<T> : IArrayWriter<T>
    {
        private readonly ThreadLocal<IArrayWriter<T>> _threadLocalizedWriter;

        public ArrayWriter(ThreadLocal<IArrayWriter<T>> threadLocalizedWriter)
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
            foreach (IArrayWriter<T> arrayWriter in _threadLocalizedWriter.Values)
            {
                arrayWriter.Dispose();
            }
        }
    }
}
