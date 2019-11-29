
using System;
using System.Linq;

using DeltaWare.SDK.Common.Enums;
using DeltaWare.SDK.Common.Interfaces;

namespace DeltaWare.SDK.Common.GenericCollections
{
    public class RecyclingQueue<T> : IRecyclingQueue<T>
    {
        private const int InitialIndex = 0;

        private readonly T[] _queue;

        private int _index = InitialIndex;

        public int Capacity => _queue.Length;

        public int Count => _queue.Count(q => !(q is null));

        public T this[int index]
        {
            get => _queue[GetOffsetIndex(index)];
            set => _queue[GetOffsetIndex(index)] = value;
        } 

        public RecyclingQueue(int capacity)
        {
            _queue = new T[capacity];
        }

        public void Add(T item)
        {
            _queue[_index] = item;

            _index++;

            if (_index >= Capacity)
            {
                _index = InitialIndex;
            }
        }

        public void AddMany(T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _queue.Length; i++)
            {
                _queue[i] = default;
            }

            _index = InitialIndex;
        }

        public T[] GetRange(int index, int count , SortDirection sortDirection = SortDirection.Ascending)
        {
            if (count > Capacity)
            {
                throw new ArgumentException("Count must be less than or equal to Capacity");
            }

            T[] values = new T[count];

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                {
                    for (int i = 0; i < count; i++)
                    {
                        values[i] = this[i];
                    }

                    break;
                }
                case SortDirection.Descending:
                {
                    for (int i = 0; i < count; i--)
                    {
                        values[i] = this[i];
                    }

                    break;
                }
            }

            return values;
        }

        public IRecyclingQueue<T> Expand(int newCapacity)
        {
            if (Capacity <= newCapacity)
            {
                throw new ArgumentException("The new Capacity must be greater than the existing.");
            }

            IRecyclingQueue<T> recyclingQueue = new RecyclingQueue<T>(newCapacity);

            for (int i = 0; i < Count; i++)
            {
                recyclingQueue.Add(this[i]);
            }

            return recyclingQueue;
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
