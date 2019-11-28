
using System;

using DeltaWare.SDK.Base.Interfaces;

namespace DeltaWare.SDK.Base.Collections
{
    public class RecyclingQueue<T> : IRecyclingQueue<T>
    {
        private readonly T[] _queue;

        private int _index;

        public int Capacity => _queue.Length;

        public T this[int index]
        {
            get => _queue[GetIndexOffset(index)];
            set => _queue[GetIndexOffset(index)] = value;
        } 

        public RecyclingQueue(int capacity)
        {
            _queue = new T[capacity];
        }

        public void Add(T item)
        {
            _queue[_index] = item;

            _index++;

            if (_index >= Capacity - 1)
            {
                _index = 0;
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

            _index = 0;
        }

        public T[] GetRange(int index, int count)
        {
            throw new NotImplementedException();
        }

        private int GetIndexOffset(int index)
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
