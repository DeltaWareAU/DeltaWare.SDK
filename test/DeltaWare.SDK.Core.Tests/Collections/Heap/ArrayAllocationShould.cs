using DeltaWare.SDK.Core.Collections.Parallel.Allocation;
using Shouldly;
using Xunit;

namespace DeltaWare.SDK.Core.Tests.Collections.Heap
{
    public class ArrayAllocationShould
    {
        [Fact]
        public void CalculateNextIndex()
        {
            ArrayAllocation[] innerAllocations = new[]
            {
                new ArrayAllocationWrapper(0, 100000, 10),
                new ArrayAllocationWrapper(100000, 100000, 10)
            };
            
            ArrayAllocationWrapper<object> allocation1 = new ArrayAllocationWrapper<object>(new object[10], 0, 10, innerAllocations);
            ArrayAllocationWrapper<object> allocation2 = new ArrayAllocationWrapper<object>(new object[10], 100000, 10, innerAllocations);

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation1.TryGetAllocatedIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation1.AllocationStart);
            }

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation2.TryGetAllocatedIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation2.AllocationStart);
            }

            allocation1 = new ArrayAllocationWrapper<object>(new object[10], 0, 5);
            allocation2 = new ArrayAllocationWrapper<object>(new object[10], 1000, 5);

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation1.TryGetAllocatedIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation1.AllocationStart);
            }

            allocation1.Position.ShouldBe(allocation1.AllocationStart + allocation1.Length);

            for (int i = 0; i < allocation1.Length; i++)
            {
                allocation2.TryGetAllocatedIndex(out int index).ShouldBeTrue();

                index.ShouldBe(i + allocation2.AllocationStart);
            }

            allocation2.Position.ShouldBe(allocation2.AllocationStart + allocation2.Length);
        }
    }

    public class ArrayAllocationWrapper<T>: ArrayAllocation<T>
    {
        public ArrayAllocationWrapper(T[] arrayAccessor, int allocationStart, int length) : base(arrayAccessor, allocationStart, length)
        {
        }

        public ArrayAllocationWrapper(T[] arrayAccessor, int allocationStart, int length, ArrayAllocation[] arrayAllocations) : base(arrayAccessor, allocationStart, length, arrayAllocations)
        {
        }

        public new bool TryGetAllocatedIndex(out int index)
        {
            return base.TryGetAllocatedIndex(out index);
        }
    }

    public class ArrayAllocationWrapper : ArrayAllocation
    {
        public ArrayAllocationWrapper(int allocationStart, int length, int position) : base(allocationStart, length)
        {
            Position = position;
        }
    }
}
