namespace DeltaWare.SDK.Core.Collections.Heap.Allocation
{
    internal class HeapAllocation
    {
        public int Length { get; }

        public int Count { get; protected set; }

        public HeapAllocation(int length)
        {
            Length = length;
        }
    }
}
