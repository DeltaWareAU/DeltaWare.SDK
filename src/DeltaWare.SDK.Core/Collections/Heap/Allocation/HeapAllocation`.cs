using System;
using System.Reflection.Metadata.Ecma335;

namespace DeltaWare.SDK.Core.Collections.Heap.Allocation
{
    internal abstract class HeapAllocation<T> : HeapAllocation
    {
        private readonly HeapAllocation[] _heapAllocations;

        protected T[] HeapAccessor { get; }

        public int AllocationStart { get; }

        public int AllocationEnd { get; }

        protected HeapAllocation(T[] heapAccessor, int allocationStart, int length) : base(length)
        {
            HeapAccessor = heapAccessor;
            AllocationStart = allocationStart;
            AllocationEnd = allocationStart + length;
            _heapAllocations = Array.Empty<HeapAllocation>();
        }

        protected HeapAllocation(T[] heapAccessor, int allocationStart, int length, HeapAllocation[] heapAllocations) : base(length)
        {
            HeapAccessor = heapAccessor;
            AllocationStart = allocationStart;
            AllocationEnd = allocationStart + length;
            _heapAllocations = heapAllocations;
        }
        
        protected int GetAllocatedHeapIndex(int position)
        {
            position += AllocationStart;

            if (position > AllocationEnd)
            {
                return -1;
            }

            if (_heapAllocations.Length == 0)
            {
                return position;
            }

            return WithHeapAllocations(position);
        }

        private int WithHeapAllocations(int position)
        {
            int offset = 0;
            int heapIndex = 0;

            do
            {
                if (position < _heapAllocations[heapIndex].Count)
                {
                    return position + offset;
                }

                position -= _heapAllocations[heapIndex].Count;

                if (position < 0)
                {
                    return -1;
                }

                offset += _heapAllocations[heapIndex].Length;

                heapIndex++;
            }
            while (heapIndex < _heapAllocations.Length);

            return -1;
        }
    }
}
