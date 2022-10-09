using DeltaWare.SDK.Core.Collections.Heap.Allocation;
using Shouldly;
using Xunit;

namespace DeltaWare.SDK.Core.Tests.Collections.Heap
{
    public class HeapAllocationShould
    {
        [Fact]
        public void CalculateNextIndex()
        {
            HeapAllocation[] innerAllocations = new[]
            {
                new HeapAllocationWrapper(0, 100000, 10),
                new HeapAllocationWrapper(100000, 100000, 10)
            };
            
            HeapAllocationWrapper<object> allocation1 = new HeapAllocationWrapper<object>(new object[10], 0, 10, innerAllocations);
            HeapAllocationWrapper<object> allocation2 = new HeapAllocationWrapper<object>(new object[10], 100000, 10, innerAllocations);

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation1.TryGetAllocatedHeapIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation1.AllocationStart);
            }

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation2.TryGetAllocatedHeapIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation2.AllocationStart);
            }

            allocation1 = new HeapAllocationWrapper<object>(new object[10], 0, 5);
            allocation2 = new HeapAllocationWrapper<object>(new object[10], 1000, 5);

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation1.TryGetAllocatedHeapIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation1.AllocationStart);
            }

            allocation1.Position.ShouldBe(allocation1.AllocationStart + allocation1.Length);

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation2.TryGetAllocatedHeapIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation2.AllocationStart);
            }

            allocation2.Position.ShouldBe(allocation2.AllocationStart + allocation2.Length);
        }
    }

    public class HeapAllocationWrapper<T>: HeapAllocation<T>
    {
        public HeapAllocationWrapper(T[] heapAccessor, int allocationStart, int length) : base(heapAccessor, allocationStart, length)
        {
        }

        public HeapAllocationWrapper(T[] heapAccessor, int allocationStart, int length, HeapAllocation[] heapAllocations) : base(heapAccessor, allocationStart, length, heapAllocations)
        {
        }

        public new bool TryGetAllocatedHeapIndex(out int index)
        {
            return base.TryGetAllocatedHeapIndex(out index);
        }
    }

    public class HeapAllocationWrapper : HeapAllocation
    {
        public HeapAllocationWrapper(int allocationStart, int length, int position) : base(allocationStart, length)
        {
            Position = position;
        }
    }
}
