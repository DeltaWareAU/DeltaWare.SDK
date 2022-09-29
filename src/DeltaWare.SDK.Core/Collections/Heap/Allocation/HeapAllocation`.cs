namespace DeltaWare.SDK.Core.Collections.Heap.Allocation
{
    internal abstract class HeapAllocation<T>
    {
        private int _actorIndex = 0;
        private int _offset = 0;


        private HeapAllocation[]? _heapAllocations;

        protected T[] HeapAccessor { get; }

        public int AllocationStart { get; }

        public int AllocationEnd { get; }

        public int Length { get; }

        protected HeapAllocation(T[] heapAccessor, int allocationStart, int length)
        {
            HeapAccessor = heapAccessor;
            AllocationStart = allocationStart;
            AllocationEnd = allocationStart + length;
            Length = length;
            _heapAllocations = null;
        }        
        
        protected HeapAllocation(T[] heapAccessor, int allocationStart, int length, HeapAllocation[] heapAllocations)
        {
            HeapAccessor = heapAccessor;
            AllocationStart = allocationStart;
            AllocationEnd = allocationStart + length;
            Length = length;
            _heapAllocations = heapAllocations;
        }

        protected int GetAllocatedHeapIndex(int position)
        {
            position += AllocationStart;

            if (position > AllocationEnd)
            {
                return -1;
            }

            if (_heapAllocations is null)
            {
                return position;
            }

            return WithHeapAllocations(position);
        }
        
        private int WithHeapAllocations(int position)
        {
            int offset = 0;
            int actorIndex = 0;

            do
            {
                if (position < _heapAllocations[actorIndex].Count)
                {
                    return position + offset;
                }

                position -= _heapAllocations[actorIndex].Count;

                if (position < 0)
                {
                    return -1;
                }

                offset += _heapAllocations[actorIndex].Length;

                actorIndex++;
            }
            while (actorIndex < _heapAllocations.Length);

            return -1;
        }
    }
}
