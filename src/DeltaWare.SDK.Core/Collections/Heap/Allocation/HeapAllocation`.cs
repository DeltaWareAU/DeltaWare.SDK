using System;

namespace DeltaWare.SDK.Core.Collections.Heap.Allocation
{
    public abstract class HeapAllocation<T> : HeapAllocation
    {
        private readonly HeapAllocation[] _heapAllocations;

        protected T[] HeapAccessor { get; }

        protected HeapAllocation(T[] heapAccessor, int allocationStart, int length) : base(allocationStart, length)
        {
            HeapAccessor = heapAccessor;
            Position = AllocationStart;
            _heapAllocations = Array.Empty<HeapAllocation>();
        }

        protected HeapAllocation(T[] heapAccessor, int allocationStart, int length, HeapAllocation[] heapAllocations) : base(allocationStart, length)
        {
            HeapAccessor = heapAccessor;
            Position = AllocationStart;
            _heapAllocations = heapAllocations;
        }

        protected bool TryGetAllocatedHeapIndex(out int index)
        {
            if (Position >= AllocationEnd)
            {
                index = -1;

                return false;
            }

            if (_heapAllocations.Length != 0)
            {
                bool result = WithHeapAllocations(out index);

                Position++;

                return result;
            }

            index = Position;

            Position++;

            return true;
        }

        private int _heapIndex;

        private int _offset;

        private bool WithHeapAllocations(out int value)
        {
            do
            {
                int index = Position + _offset;

                if (index < _heapAllocations[_heapIndex].Position)
                {
                    value = index;

                    return true;
                }

                _offset = _heapAllocations[_heapIndex].AllocationEnd - Position;

                _heapIndex++;
            }
            while (_heapIndex < _heapAllocations.Length);

            value = -1;

            return false;
        }
    }
}
