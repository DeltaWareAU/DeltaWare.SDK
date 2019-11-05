using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    public class HilbertArray<T> : HilbertArrayBase
    {
        public readonly HilbertVector<T>[] Vectors;

        public HilbertVector<T> this[long index] => Vectors[index];

        public readonly int Depth;

        public readonly long Length;

        private HilbertArray(HilbertVector<T>[] vectors, int depth, long length)
        {
            Vectors = vectors;

            Depth = depth;
            Length = length;
        }

        public T[] ToArray()
        {
            return Vectors.Select(v => v.Value).ToArray();
        }

        public IList<T> ToList()
        {
            return Vectors.Select(v => v.Value).ToList();
        }

        public static HilbertArray<T> Generate(T[] items)
        {
            long length = items.LongCount();

            int depth = 1;

            while (length > Math.Pow(4, depth))
            {
                depth++;
            }

            HilbertVector<T>[] vectors = new HilbertVector<T>[length];

            Parallel.For(0, length, index =>
            {
                GetIndexCoordinates(length, index, out long x, out long y);

                vectors[index] = new HilbertVector<T>(items[index], (int)x, (int)y, index);
            });

            return new HilbertArray<T>(vectors, depth, length);
        }
    }
}
