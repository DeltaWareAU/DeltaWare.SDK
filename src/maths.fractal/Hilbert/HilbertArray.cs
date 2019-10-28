using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    public struct HilbertArray<T>
    {
        private static Dictionary<HilbertDirection, HilbertDirection[]> Order = new Dictionary<HilbertDirection, HilbertDirection[]>
        {
            {HilbertDirection.Up, new []{HilbertDirection.Left, HilbertDirection.Left, HilbertDirection.Down} },
            {HilbertDirection.Left, new []{HilbertDirection.Up, HilbertDirection.Up, HilbertDirection.Right} },
            {HilbertDirection.Down, new []{HilbertDirection.Right, HilbertDirection.Right, HilbertDirection.Up} },
            {HilbertDirection.Right, new []{HilbertDirection.Down, HilbertDirection.Down, HilbertDirection.Left} }
        };

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
                d2xy(length, index, out long x, out long y);

                vectors[index] = new HilbertVector<T>(items[index], x, y, index);
            });

            //for (int i = 0; i < length; i++)
            //{
                //d2xy(length, i, out long x, out long y);

                //vectors[i] = new HilbertVector<T>(items[i], x, y, i);
            //}

            return new HilbertArray<T>(vectors, depth, length);
        }

        //convert d to (x,y)
        public static void d2xy(long depth, long currentIndex, out long x, out long y)
        {
            x = 0;
            y = 0;

            long rx;
            long ry;
            long t = currentIndex;
            
            for (int s = 1; s < depth; s *= 2)
            {
                rx = 1 & (t / 2);
                ry = 1 & (t ^ rx);

                rot(s, ref x, ref y, rx, ry);

                x += s * rx;
                y += s * ry;
                t /= 4;
            }
        }
        private static void rot(long n, ref long x, ref long y, long rx, long ry)
        {
            if (ry == 0)
            {
                if (rx == 1)
                {
                    x = n - 1 - x;
                    y = n - 1 - y;
                }

                long tempX = x;
                x = y;
                y = tempX;
            }
        }
    }
}
