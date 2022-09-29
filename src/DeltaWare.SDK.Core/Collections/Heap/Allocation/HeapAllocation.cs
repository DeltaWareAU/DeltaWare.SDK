namespace DeltaWare.SDK.Core.Collections.Heap.Allocation
{
    internal class HeapAllocation
    {
        public int Length { get; }

        public int Count { get; }

        public HeapAllocation(int length, int count)
        {
            Length = length;
            Count = count;
        }
    }
}
