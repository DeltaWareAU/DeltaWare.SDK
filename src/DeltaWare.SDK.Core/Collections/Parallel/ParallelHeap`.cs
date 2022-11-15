using DeltaWare.SDK.Core.Collections.Parallel.Exceptions;
using DeltaWare.SDK.Core.Collections.Parallel.Reader;
using DeltaWare.SDK.Core.Collections.Parallel.Writer;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Parallel
{
    /// <summary>
    /// A High Performance Thread Safe Parallel Array.
    /// </summary>
    /// <typeparam name="T">The Type Stored in the Collection</typeparam>
    [DebuggerDisplay("Length:{Length} Count:{Count} - Active[{ActiveReaders}:Readers/{ActiveWriters}:Writers]")]
    public sealed class ParallelArray<T> : IEnumerable<T>
    {
        private readonly T[] _internalHeap;

        private readonly ConcurrentQueue<InternalArrayReader<T>> _unallocatedArrayReaderQueue = new();
        private readonly ConcurrentQueue<InternalArrayWriter<T>> _unallocatedArrayWriterQueue = new();

        private InternalArrayWriter<T>[] _internalArrayWriters = Array.Empty<InternalArrayWriter<T>>();
        private InternalArrayReader<T>[] _internalArrayReaders = Array.Empty<InternalArrayReader<T>>();

        /// <summary>
        /// The Length of the <see cref="ParallelArray{T}"/>.
        /// </summary>
        public int Length => _internalHeap.Length;

        /// <summary>
        /// The Number of <see cref="object"/>s stored in the <see cref="ParallelArray{T}"/>.
        /// </summary>
        public int Count => _internalArrayWriters.Sum(w => w.Count);

        /// <summary>
        /// How many <see cref="IArrayWriter{T}"/> are Active for the <see cref="ParallelArray{T}"/>.
        /// </summary>
        public int ActiveWriters => _internalArrayWriters.Length;

        /// <summary>
        /// How many <see cref="IArrayReader{T}"/> are Active for the <see cref="ParallelArray{T}"/>.
        /// </summary>
        public int ActiveReaders => _internalArrayReaders.Length;

        /// <summary>
        /// Instantiates a new Instance of the <see cref="ParallelArray{T}"/>.
        /// </summary>
        /// <param name="length">The Length of the Array.</param>
        /// <remarks>
        /// The Array's Length is Split equally by the Number of Threads.<para/>
        /// <b>Example:</b> If you have a Length of 1000 and have 10 Threads Assigned to the <see cref="IArrayWriter{T}"/> each Writer may only Write a Maximum of 100 <see cref="object"/>s.
        /// </remarks>
        public ParallelArray(int length)
        {
            _internalHeap = new T[length];
        }

        public void Clear()
        {
            foreach (InternalArrayReader<T> heapReader in _internalArrayReaders)
            {
                heapReader.Dispose();
            }

            foreach (InternalArrayWriter<T> heapWriter in _internalArrayWriters)
            {
                heapWriter.Dispose();
            }

            _unallocatedArrayReaderQueue.Clear();
            _unallocatedArrayWriterQueue.Clear();
            _internalArrayReaders = Array.Empty<InternalArrayReader<T>>();
            _internalArrayWriters = Array.Empty<InternalArrayWriter<T>>();
        }

        public IArrayReader<T> CreateReader(int allocatedThreads)
        {
            if (_unallocatedArrayReaderQueue.Count > 0)
            {
                throw ParallelArrayExceptions.UnallocatedReaderException();
            }

            InitializeInternalHeapReaders(allocatedThreads);

            return BuildHeapReader();
        }

        public IArrayWriter<T> CreateWriter(int maxThreads)
        {
            if (_unallocatedArrayReaderQueue.Count > 0)
            {
                throw ParallelArrayExceptions.UnallocatedReaderException();
            }

            if (_unallocatedArrayWriterQueue.Count > 0)
            {
                _unallocatedArrayWriterQueue.Clear();
            }

            InitializeInternalHeapWriters(maxThreads);

            return BuildHeapWriter();
        }

        #region Heap Reader

        private void InitializeInternalHeapReaders(int totalThreads)
        {
            int allocation = Math.DivRem(Count, totalThreads, out int remainder);

            _internalArrayReaders = new InternalArrayReader<T>[totalThreads];

            for (int i = 0; i < totalThreads; i++)
            {
                InternalArrayReader<T> arrayReader;

                if (i == 0)
                {
                    arrayReader = new InternalArrayReader<T>(_internalHeap, 0, allocation + remainder, _internalArrayWriters);
                }
                else
                {
                    arrayReader = new InternalArrayReader<T>(_internalHeap, allocation * i + remainder, allocation, _internalArrayWriters);
                }

                _internalArrayReaders[i] = arrayReader;
                _unallocatedArrayReaderQueue.Enqueue(arrayReader);
            }
        }

        private IArrayReader<T> BuildHeapReader()
        {
            ThreadLocal<IArrayReader<T>> threadLocalizedWriter = new ThreadLocal<IArrayReader<T>>(GetNextReader, true);

            return new ArrayReader<T>(threadLocalizedWriter);

            IArrayReader<T> GetNextReader()
            {
                if (_unallocatedArrayReaderQueue.TryDequeue(out InternalArrayReader<T> value))
                {
                    return value;
                }

                throw ParallelArrayExceptions.UnallocatedThreadException(Thread.CurrentThread);
            }
        }

        #endregion

        #region Heap Writers

        private void InitializeInternalHeapWriters(int totalThreads)
        {
            int offsetMultiplier = Math.DivRem(Length, totalThreads, out _);

            _internalArrayWriters = new InternalArrayWriter<T>[totalThreads];

            for (int i = 0; i < totalThreads; i++)
            {
                InternalArrayWriter<T> arrayWriter = new InternalArrayWriter<T>(_internalHeap, offsetMultiplier * i, offsetMultiplier);

                _internalArrayWriters[i] = arrayWriter;
                _unallocatedArrayWriterQueue.Enqueue(arrayWriter);
            }
        }

        private IArrayWriter<T> BuildHeapWriter()
        {
            ThreadLocal<IArrayWriter<T>> threadLocalizedWriter = new ThreadLocal<IArrayWriter<T>>(GetWriter, true);

            return new ArrayWriter<T>(threadLocalizedWriter);

            IArrayWriter<T> GetWriter()
            {
                if (_unallocatedArrayWriterQueue.TryDequeue(out InternalArrayWriter<T> writer))
                {
                    return writer;
                }

                throw ParallelArrayExceptions.UnallocatedThreadException(Thread.CurrentThread);
            }
        }

        #endregion

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            using IArrayReader<T> reader = CreateReader(1);

            while (reader.TryRead(out T value))
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
