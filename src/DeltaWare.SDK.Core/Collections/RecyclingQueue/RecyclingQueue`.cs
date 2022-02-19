using System;
using System.Collections.Generic;
using System.Linq;

namespace DeltaWare.SDK.Core.Collections.RecyclingQueue
{
    public class RecyclingQueue<TValue> : IRecyclingQueue<TValue>
    {
        private const int InitialIndex = 0;

        private readonly TValue[] _queue;

        private int _index = InitialIndex;

        public int Capacity => _queue.Length;

        public int Count => _queue.Count(q => q != null);

        public TValue this[int index]
        {
            get => _queue[GetOffsetIndex(index)];
            set => _queue[GetOffsetIndex(index)] = value;
        }

        public RecyclingQueue(int capacity)
        {
            _queue = new TValue[capacity];
        }

        public virtual void Add(TValue item)
        {
            _queue[_index] = item;

            _index++;

            if (_index >= Capacity)
            {
                _index = InitialIndex;
            }
        }

        public virtual void AddMany(TValue[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public virtual void Clear()
        {
            for (int i = 0; i < _queue.Length; i++)
            {
                _queue[i] = default;
            }

            _index = InitialIndex;
        }

        public IRecyclingQueue<TValue> Expand(int newCapacity)
        {
            if (Capacity <= newCapacity)
            {
                throw new ArgumentException("The new Capacity must be greater than the existing.");
            }

            IRecyclingQueue<TValue> recyclingQueue = new RecyclingQueue<TValue>(newCapacity);

            for (int i = 0; i < Count; i++)
            {
                recyclingQueue.Add(this[i]);
            }

            return recyclingQueue;
        }

        public virtual TValue[] GetRange(int index, int count, SortDirection direction = SortDirection.Ascending)
        {
            if (count > Capacity)
            {
                throw new ArgumentException("Count must be less than or equal to Capacity");
            }

            TValue[] values = new TValue[count];

            switch (direction)
            {
                case SortDirection.Ascending:
                    for (int i = 0; i < count; i++)
                    {
                        values[i] = this[i];
                    }

                    break;

                case SortDirection.Descending:
                    for (int i = 0; i < count; i--)
                    {
                        values[i] = this[i];
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return values;
        }

        public virtual TValue[] ToArray()
        {
            return _queue.ToArray();
        }

        public virtual List<TValue> ToList()
        {
            return _queue.ToList();
        }

        private int GetOffsetIndex(int index)
        {
            index += _index;

            if (index > Capacity - 1)
            {
                index -= Capacity;
            }

            return index;
        }
    }
}