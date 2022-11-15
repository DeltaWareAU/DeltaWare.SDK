using DeltaWare.SDK.Core.Collections.Parallel.Allocation;
using DeltaWare.SDK.Core.Collections.Parallel.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DeltaWare.SDK.Core.Collections.Parallel.Reader
{
    [DebuggerDisplay("Length:{Length} Position:{Position} - AllocationStart:{AllocationStart} - AllocationEnd:{AllocationEnd}")]
    internal sealed class InternalArrayReader<T> : ArrayAllocation<T>, IArrayReader<T>
    {
        public InternalArrayReader(T[] arrayAccessor, int allocationStart, int length, ArrayAllocation[] arrayAllocations) : base(arrayAccessor, allocationStart, length, arrayAllocations)
        {
        }

        public bool TryRead(out T? value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("InternalArrayWriter");
            }

            if (!TryGetAllocatedIndex(out int index))
            {
                value = default;

                return false;
            }

            value = ArrayAccessor[index];

            if (value == null)
            {
                throw ParallelArrayExceptions.UnallocatedReadAccess(index);
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
