using DeltaWare.SDK.Core.Collections.Heap.Allocation;
using DeltaWare.SDK.Core.Collections.Heap.Exceptions;
using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Core.Collections.Heap.Writer
{
    [DebuggerDisplay("Length:{Length} Count:{Count} - AllocationStart:{AllocationStart} - AllocationEnd:{AllocationEnd}")]
    internal sealed class InternalHeapWriter<T> : HeapAllocation<T>, IHeapWriter<T>
    {
        public int Count { get; private set; }

        public InternalHeapWriter(T[] heapAccessor, int allocationStart, int length) : base(heapAccessor, allocationStart, length)
        {
        }

        public void Write(T value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("InternalHeapWriter");
            }

            if (!TryGetAllocatedHeapIndex(out int index))
            {
                throw UnallocatedHeapAccessException.UnallocatedWriteAccess();
            }

            Count++;

            if (value == null)
            {
                throw new ArgumentNullException();
            }

            HeapAccessor[index] = value;
        }

        private bool _disposed;

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
