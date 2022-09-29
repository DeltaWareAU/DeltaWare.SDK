using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DeltaWare.SDK.Core.Collections.Heap.Allocation;

namespace DeltaWare.SDK.Core.Collections.Heap.Reader
{
    [DebuggerDisplay("Length:{Length} Position:{Position} - AllocationStart:{AllocationStart} - AllocationEnd:{AllocationEnd}")]
    internal sealed class InternalHeapReader<T>: HeapAllocation<T>, IHeapReader<T>
    {
        private readonly object _lock = new();

        public int Position { get; private set; } = -1;

        public InternalHeapReader(T[] heapAccessor, int allocationStart, int length, HeapAllocation[] heapAllocations) : base(heapAccessor, allocationStart, length, heapAllocations)
        {
        }

        public bool TryRead(out T? value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("InternalHeapWriter");
            }

            lock (_lock)
            {
                Position++;

                //if (Position >= Length)
                //{
                //    value = default;

                //    return false;
                //}

                int index = GetAllocatedHeapIndex(Position);

                if (index < 0)
                {
                    value = default;

                    return false;
                }

                value = HeapAccessor[index];

                if (value is null)
                {
                    throw new ArgumentException($"UNALLOCATED HEAP INDEX REACHED @ {index}");
                }

                return true;
            }
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
