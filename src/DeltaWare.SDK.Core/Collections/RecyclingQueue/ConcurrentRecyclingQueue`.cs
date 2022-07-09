using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.Core.Collections.RecyclingQueue
{
    public class ConcurrentRecyclingQueue<TValue> : RecyclingQueue<TValue>
    {
        private readonly object _concurrencyLock = new object();

        public ConcurrentRecyclingQueue(int capacity) : base(capacity)
        {
        }

        public override void Add(TValue item)
        {
            lock (_concurrencyLock)
            {
                base.Add(item);
            }
        }

        public override void AddMany(TValue[] items)
        {
            lock (_concurrencyLock)
            {
                base.AddMany(items);
            }
        }

        public override void Clear()
        {
            lock (_concurrencyLock)
            {
                base.Clear();
            }
        }

        public new IRecyclingQueue<TValue> Expand(int newCapacity)
        {
            if (Capacity <= newCapacity)
            {
                throw new ArgumentException("The new Capacity must be greater than the existing.");
            }

            lock (_concurrencyLock)
            {
                IRecyclingQueue<TValue> recyclingQueue = new ConcurrentRecyclingQueue<TValue>(newCapacity);

                for (int i = 0; i < Count; i++)
                {
                    recyclingQueue.Add(this[i]);
                }

                return recyclingQueue;
            }
        }

        public override TValue[] GetRange(int index, int count, SortDirection direction = SortDirection.Ascending)
        {
            lock (_concurrencyLock)
            {
                return base.GetRange(index, count, direction);
            }
        }

        public override TValue[] ToArray()
        {
            lock (_concurrencyLock)
            {
                return base.ToArray();
            }
        }

        public override List<TValue> ToList()
        {
            lock (_concurrencyLock)
            {
                return base.ToList();
            }
        }
    }
}