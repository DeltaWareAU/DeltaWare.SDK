using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    public struct HilbertArray<T>
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

        private static void GetIndexCoordinates(long length, long currentIndex, out long x, out long y)
        {
            x = 0;
            y = 0;

            for (long index = 1; index < length; index *= 2)
            {
                long flipX = 1 & (currentIndex / 2);
                long flipY = 1 & (currentIndex ^ flipX);

                Rotate(index, ref x, ref y, flipX, flipY);

                x += index * flipX;
                y += index * flipY;

                currentIndex /= 4;
            }
        }

        private static void Rotate(long index, ref long x, ref long y, long flipX, long flipY)
        {
            if (flipY != 0) 
                return;

            if (flipX == 1)
            {
                x = index - 1 - x;
                y = index - 1 - y;
            }

            long tempX = x;
            x = y;
            y = tempX;
        }
    }
}
