using DeltaWare.SDK.Core.Collections.Heap.Exceptions;
using DeltaWare.SDK.Core.Collections.Heap.Reader;
using DeltaWare.SDK.Core.Collections.Heap.Writer;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Heap
{
    /// <summary>
    /// An Extremely High Performance Thread Safe Parallel Collection.
    /// </summary>
    /// <typeparam name="T">The Type Stored in the Collection</typeparam>
    public class ParallelHeap<T> : IEnumerable<T>
    {
        private readonly ConcurrentQueue<InternalHeapReader<T>> _heapReaderQueue = new();
        private readonly ConcurrentQueue<InternalHeapWriter<T>> _heapWriterQueue = new();

        private InternalHeapWriter<T>[] _internalHeapWriters = Array.Empty<InternalHeapWriter<T>>();
        private InternalHeapReader<T>[] _internalHeapReaders = Array.Empty<InternalHeapReader<T>>();

        private readonly T[] _internalHeap;

        public int Length => _internalHeap.Length;

        public int Count => _internalHeapWriters.Sum(w => w.Count);

        public int ActiveWriters => _internalHeapWriters.Length;

        public int ActiveReaders => _internalHeapReaders.Length;

        public ParallelHeap(int length)
        {
            _internalHeap = new T[length];
        }

        public void Reset()
        {
            foreach (InternalHeapReader<T> heapReader in _internalHeapReaders)
            {
                heapReader.Dispose();
            }

            foreach (InternalHeapWriter<T> heapWriter in _internalHeapWriters)
            {
                heapWriter.Dispose();
            }

            _heapReaderQueue.Clear();
            _heapWriterQueue.Clear();
            _internalHeapReaders = Array.Empty<InternalHeapReader<T>>();
            _internalHeapWriters = Array.Empty<InternalHeapWriter<T>>();
        }

        public IHeapReader<T> CreateReader(int totalThreads)
        {
            if (_heapReaderQueue.Count > 0)
            {
                throw HeapAccessViolationException.UnAllocatedReaders();
            }

            InitializeInternalHeapReaders(totalThreads);

            return BuildHeapReader();
        }

        public IHeapWriter<T> CreateWriter(int totalThreads)
        {
            if (_heapReaderQueue.Count > 0)
            {
                throw HeapAccessViolationException.UnAllocatedReaders();
            }

            if (_heapWriterQueue.Count > 0)
            {
                _heapWriterQueue.Clear();
            }

            InitializeInternalHeapWriters(totalThreads);

            return BuildHeapWriter();
        }

        #region Heap Reader

        private void InitializeInternalHeapReaders(int totalThreads)
        {
            int allocation = Math.DivRem(Count, totalThreads, out int remainder);

            _internalHeapReaders = new InternalHeapReader<T>[totalThreads];

            for (int i = 0; i < totalThreads; i++)
            {
                InternalHeapReader<T> heapReader;

                if (i == 0)
                {
                    heapReader = new InternalHeapReader<T>(_internalHeap, 0, allocation + remainder, _internalHeapWriters);
                }
                else
                {
                    heapReader = new InternalHeapReader<T>(_internalHeap, (allocation * i) + remainder, allocation, _internalHeapWriters);
                }

                _internalHeapReaders[i] = heapReader;
                _heapReaderQueue.Enqueue(heapReader);
            }
        }

        private IHeapReader<T> BuildHeapReader()
        {
            ThreadLocal<IHeapReader<T>> threadLocalizedWriter = new ThreadLocal<IHeapReader<T>>(GetNextReader, true);

            return new HeapReader<T>(threadLocalizedWriter);

            IHeapReader<T> GetNextReader()
            {
                if (_heapReaderQueue.TryDequeue(out InternalHeapReader<T> value))
                {
                    return value;
                }

                throw HeapAccessViolationException.UnexpectedThread(Thread.CurrentThread);
            }
        }

        #endregion

        #region Heap Writers

        private void InitializeInternalHeapWriters(int totalThreads)
        {
            int offsetMultiplier = Math.DivRem(Length, totalThreads, out _);

            _internalHeapWriters = new InternalHeapWriter<T>[totalThreads];

            for (int i = 0; i < totalThreads; i++)
            {
                InternalHeapWriter<T> heapWriter = new InternalHeapWriter<T>(_internalHeap, offsetMultiplier * i, offsetMultiplier);

                _internalHeapWriters[i] = heapWriter;
                _heapWriterQueue.Enqueue(heapWriter);
            }
        }

        private IHeapWriter<T> BuildHeapWriter()
        {
            ThreadLocal<IHeapWriter<T>> threadLocalizedWriter = new ThreadLocal<IHeapWriter<T>>(GetWriter, true);

            return new HeapWriter<T>(threadLocalizedWriter);

            IHeapWriter<T> GetWriter()
            {
                if (_heapWriterQueue.TryDequeue(out InternalHeapWriter<T> writer))
                {
                    return writer;
                }

                throw HeapAccessViolationException.UnexpectedThread(Thread.CurrentThread);
            }
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            using IHeapReader<T> reader = CreateReader(1);

            while (reader.TryRead(out T value))
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
