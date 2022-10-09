using DeltaWare.SDK.Core.Collections.Heap.Allocation;
using DeltaWare.SDK.Core.Collections.Heap.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DeltaWare.SDK.Core.Collections.Heap.Reader
{
    [DebuggerDisplay("Length:{Length} Position:{Position} - AllocationStart:{AllocationStart} - AllocationEnd:{AllocationEnd}")]
    internal sealed class InternalHeapReader<T> : HeapAllocation<T>, IHeapReader<T>
    {
        public InternalHeapReader(T[] heapAccessor, int allocationStart, int length, HeapAllocation[] heapAllocations) : base(heapAccessor, allocationStart, length, heapAllocations)
        {
        }

        public bool TryRead(out T? value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("InternalHeapWriter");
            }

            if (!TryGetAllocatedHeapIndex(out int index))
            {
                value = default;

                return false;
            }

            value = HeapAccessor[index];

            if (value == null)
            {
                throw UnallocatedHeapAccessException.UnallocatedReadAccess(index);
            }

            return true;
        }

        private bool _disposed;

        public void Dispose()
        {
            _disposed = true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (TryRead(out T value))
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
