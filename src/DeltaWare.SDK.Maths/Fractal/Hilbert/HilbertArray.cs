using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Maths.Fractal.Hilbert
{
    public class HilbertArray<T>: HilbertArrayBase, IEnumerable
    {
        private HilbertVector<T>[] _vectors;

        public HilbertVector<T> this[long index] => _vectors[index];

        private HilbertArray(int depth, long length)
        {
            Depth = depth;
            Length = length;
        }

        public T[] ToArray()
        {
            return _vectors.Select(v => v.Value).ToArray();
        }

        public IList<T> ToList()
        {
            return _vectors.Select(v => v.Value).ToList();
        }

        public static HilbertArray<T> Generate(T[] items)
        {
            long length = items.LongCount();

            int depth = 1;

            while(length > Math.Pow(4, depth))
            {
                depth++;
            }

            HilbertArray<T> hilbertArray = new HilbertArray<T>(depth, length)
            {
                _vectors = new HilbertVector<T>[length]
            };

            Parallel.For(0, length, index =>
            {
                hilbertArray._vectors[index] = hilbertArray.GenerateVector(items[index], index);
            });

            return hilbertArray;
        }

        public static async Task<HilbertArray<T>> GenerateAsync(T[] items)
        {
            long length = items.LongCount();

            int depth = 1;

            while(length > Math.Pow(4, depth))
            {
                depth++;
            }

            HilbertArray<T> hilbertArray = new HilbertArray<T>(depth, length)
            {
                _vectors = new HilbertVector<T>[length]
            };

            Task<HilbertVector<T>>[] tasks = new Task<HilbertVector<T>>[length];

            Parallel.For(0, length, index =>
            {
                tasks[index] = hilbertArray.GenerateVectorAsync(items[index], index);
            });

            hilbertArray._vectors = await Task.WhenAll(tasks);

            return hilbertArray;
        }

        public IEnumerator GetEnumerator()
        {
            return _vectors.GetEnumerator();
        }
    }
}
