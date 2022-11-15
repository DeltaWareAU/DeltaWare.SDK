using DeltaWare.SDK.Core.Collections.Parallel.Allocation;
using DeltaWare.SDK.Core.Collections.Parallel.Exceptions;
using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Core.Collections.Parallel.Writer
{
    [DebuggerDisplay("Length:{Length} Count:{Count} - AllocationStart:{AllocationStart} - AllocationEnd:{AllocationEnd}")]
    internal sealed class InternalArrayWriter<T> : ArrayAllocation<T>, IArrayWriter<T>
    {
        public int Count { get; private set; }

        public InternalArrayWriter(T[] arrayAccessor, int allocationStart, int length) : base(arrayAccessor, allocationStart, length)
        {
        }

        public void Write(T value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("InternalArrayWriter");
            }

            if (!TryGetAllocatedIndex(out int index))
            {
                throw ParallelArrayExceptions.WriterOutOfRangeException();
            }

            Count++;

            if (value == null)
            {
                throw new ArgumentNullException();
            }

            ArrayAccessor[index] = value;
        }

        private bool _disposed;

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
