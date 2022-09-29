using System;
using System.Diagnostics;
using DeltaWare.SDK.Core.Collections.Heap.Allocation;

namespace DeltaWare.SDK.Core.Collections.Heap.Writer
{
    [DebuggerDisplay("Length:{Length} Count:{Count} - AllocationStart:{AllocationStart} - AllocationEnd:{AllocationEnd}")]
    internal sealed class InternalHeapWriter<T> : HeapAllocation<T>, IHeapWriter<T>
    {
        private readonly object _lock = new();
        
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

            int index;

            lock (_lock)
            {
                index = GetAllocatedHeapIndex(Count);

                if (index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                Count++;
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
