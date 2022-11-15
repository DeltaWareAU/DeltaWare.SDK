namespace DeltaWare.SDK.Core.Collections.Parallel.Allocation
{
    public class ArrayAllocation
    {
        public int Length { get; }

        public int AllocationStart { get; }

        public int AllocationEnd { get; }

        public int Position { get; protected set; }

        public ArrayAllocation(int allocationStart, int length)
        {
            Length = length;
            AllocationStart = allocationStart;
            AllocationEnd = allocationStart + length;
        }
    }
}
