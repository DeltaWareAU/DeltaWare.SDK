using System;

namespace DeltaWare.SDK.Core.Collections.Parallel.Allocation
{
    public abstract class ArrayAllocation<T> : ArrayAllocation
    {
        private readonly ArrayAllocation[] _arrayAllocations;

        protected T[] ArrayAccessor { get; }

        protected ArrayAllocation(T[] arrayAccessor, int allocationStart, int length) : base(allocationStart, length)
        {
            ArrayAccessor = arrayAccessor;
            Position = AllocationStart;
            _arrayAllocations = Array.Empty<ArrayAllocation>();
        }

        protected ArrayAllocation(T[] arrayAccessor, int allocationStart, int length, ArrayAllocation[] arrayAllocations) : base(allocationStart, length)
        {
            ArrayAccessor = arrayAccessor;
            Position = AllocationStart;
            _arrayAllocations = arrayAllocations;
        }

        protected bool TryGetAllocatedIndex(out int index)
        {
            if (Position >= AllocationEnd)
            {
                index = -1;

                return false;
            }

            if (_arrayAllocations.Length != 0)
            {
                bool result = WithArrayAllocations(out index);

                Position++;

                return result;
            }

            index = Position;

            Position++;

            return true;
        }

        private int _arrayIndex;

        private int _offset;

        private bool WithArrayAllocations(out int value)
        {
            do
            {
                int index = Position + _offset;

                if (index < _arrayAllocations[_arrayIndex].Position)
                {
                    value = index;

                    return true;
                }

                _offset = _arrayAllocations[_arrayIndex].AllocationEnd - Position;

                _arrayIndex++;
            }
            while (_arrayIndex < _arrayAllocations.Length);

            value = -1;

            return false;
        }
    }
}
