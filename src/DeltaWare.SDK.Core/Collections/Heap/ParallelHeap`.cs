using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DeltaWare.SDK.Core.Collections.Heap.Allocation;
using DeltaWare.SDK.Core.Collections.Heap.Reader;
using DeltaWare.SDK.Core.Collections.Heap.Writer;

namespace DeltaWare.SDK.Core.Collections.Heap
{
    public class ParallelHeap<T> : IEnumerable<T>, IDisposable
    {
        private ConcurrentQueue<InternalHeapReader<T>> _heapReaderQueue = new();
        private ConcurrentQueue<InternalHeapWriter<T>> _heapWriterQueue = new();

        private InternalHeapWriter<T>[] _internalHeapWriters = Array.Empty<InternalHeapWriter<T>>();
        private InternalHeapReader<T>[] _internalHeapReaders = Array.Empty<InternalHeapReader<T>>();

        private readonly T[] _internalHeap;

        public int Length { get; }

        public int Count => _internalHeapWriters.Sum(w => w.Count);

        public int ActiveWriters => _internalHeapWriters.Length;

        public ParallelHeap(int length)
        {
            Length = length;

            _internalHeap = new T[Length];
        }

        public IHeapReader<T> CreateReader(int totalThreads)
        {
            DisposeInternalHeapWriter();

            if (_heapReaderQueue.Count > 0)
            {
                throw new AccessViolationException();
            }

            int allocation = Math.DivRem(Count, totalThreads, out int remainder);

            _internalHeapReaders = new InternalHeapReader<T>[totalThreads];

            HeapAllocation[] heapAllocations = _internalHeapWriters.Select(w => new HeapAllocation(w.Length, w.Count)).ToArray();

            for (int i = 0; i < totalThreads; i++)
            {
                InternalHeapReader<T> heapReader;

                if (i == 0)
                {
                    heapReader = new InternalHeapReader<T>(_internalHeap, 0, allocation + remainder, heapAllocations);
                }
                else
                {
                    heapReader = new InternalHeapReader<T>(_internalHeap, (allocation * i) + remainder, allocation, heapAllocations);
                }

                _internalHeapReaders[i] = heapReader;
                _heapReaderQueue.Enqueue(heapReader);
            }

            //_heapReaderQueue = new ConcurrentQueue<InternalHeapReader<T>>(_internalHeapReaders);

            ThreadLocal<IHeapReader<T>> threadLocalizedWriter = new ThreadLocal<IHeapReader<T>>(GetNextReader);

            return new HeapReader<T>(threadLocalizedWriter);

            IHeapReader<T> GetNextReader()
            {
                if (_heapReaderQueue.TryDequeue(out InternalHeapReader<T> value))
                {
                    return value;
                }

                throw new ArgumentNullException();
            }
        }

        public IHeapWriter<T> CreateWriter(int totalThreads)
        {
            DisposeInternalHeapReader();
            
            int offsetMultiplier = Math.DivRem(Length, totalThreads, out _);

            _internalHeapWriters = new InternalHeapWriter<T>[totalThreads];

            for (int i = 0; i < totalThreads; i++)
            {
                InternalHeapWriter<T> heapWriter = new InternalHeapWriter<T>(_internalHeap, offsetMultiplier * i, offsetMultiplier);

                _internalHeapWriters[i] = heapWriter;
            }

            _heapWriterQueue = new ConcurrentQueue<InternalHeapWriter<T>>(_internalHeapWriters);

            ThreadLocal<IHeapWriter<T>> threadLocalizedWriter = new ThreadLocal<IHeapWriter<T>>(GetWriter);

            return new HeapWriter<T>(threadLocalizedWriter);
            
            IHeapWriter<T> GetWriter()
            {
                if (_heapWriterQueue.TryDequeue(out InternalHeapWriter<T> writer))
                {
                    return writer;
                }

                throw new ArgumentNullException();
            }
        }

        private bool TryCalculateHeapIndex(int index, out int actualIndex)
        {
            int actorIndex = 0;
            int offset = 0;

            do
            {
                if (index < _internalHeapWriters[actorIndex].Count)
                {
                    actualIndex = index + offset;

                    return true;
                }

                index -= _internalHeapWriters[actorIndex].Count;

                if (index < 0)
                {
                    actualIndex = -1;

                    return false;
                }

                offset += _internalHeapWriters[actorIndex].Length;

                actorIndex++;

                if (actorIndex == _internalHeapWriters.Length)
                {
                    actualIndex = -1;

                    return false;
                }
            }
            while (actorIndex < _internalHeapWriters.Length);

            actualIndex = -1;

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int index = 0;

            while (TryCalculateHeapIndex(index, out int heapIndex))
            {
                index++;

                yield return _internalHeap[heapIndex];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            DisposeInternalHeapWriter();
            DisposeInternalHeapReader();
        }

        private void DisposeInternalHeapWriter()
        {
            foreach (InternalHeapWriter<T> heapWriter in _internalHeapWriters)
            {
                heapWriter?.Dispose();
            }
        }

        private void DisposeInternalHeapReader()
        {
            foreach (InternalHeapReader<T> heapReader in _internalHeapReaders)
            {
                heapReader?.Dispose();
            }
        }
    }
}
