namespace DeltaWare.SDK.Core.Collections.Heap.Allocation
{
    public class HeapAllocation
    {
        public int Length { get; }

        public int AllocationStart { get; }

        public int AllocationEnd { get; }

        public int Position { get; protected set; }

        public HeapAllocation(int allocationStart, int length)
        {
            Length = length;
            AllocationStart = allocationStart;
            AllocationEnd = allocationStart + length;
        }
    }
}
