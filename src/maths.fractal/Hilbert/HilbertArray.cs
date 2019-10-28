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

        private readonly HilbertVector<T>[] _vectors;

        public HilbertVector<T> this[long index] => _vectors[index];

        public readonly int Depth;

        public readonly long Length;

        private HilbertArray(HilbertVector<T>[] vectors, int depth, long length)
        {
            _vectors = vectors;

            Depth = depth;
            Length = length;
        }

        public T[] ToArray()
        {
            return _vectors.Select(v => v.Item).ToArray();
        }

        public static HilbertArray<T> Generate(List<T> items)
        {
            long length = items.LongCount();

            int depth = 1;

            // Get Hilbert Depth 4^X
            while (length > Math.Pow(4, depth))
            {
                depth++;
            }

            // Get Starting Direction
            HilbertDirection direction = depth % 2 == 0 ? HilbertDirection.Up : HilbertDirection.Left;

            HilbertVector<T>[] vectors = new HilbertVector<T>[length];

            long x = 0;
            long y = 0;

            for (int i = 0; i < length; i++)
            {
                switch (direction)
                {
                    case HilbertDirection.Up:

                        break;
                    case HilbertDirection.Left:
                        break;
                    case HilbertDirection.Down:
                        break;
                    case HilbertDirection.Right:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                vectors[i] = new HilbertVector<T>(items[i], x, y, i);
            }
            
            return new HilbertArray<T>(vectors, depth, length);
        }

        private int xy2d(int n, int x, int y)
        {
            int rx;
            int ry;
            int s;
            int d = 0;

            for (s = n / 2; s > 0; s /= 2)
            {
                rx = (x & s) >> 0;
                ry = (y & s) >> 0;
                d += s * s * ((3 * rx) ^ ry);
                rot(n, ref x, ref y, rx, ry);
            }
            return d;
        }

        //convert d to (x,y)
        public static void d2xy(int n, int d, out int x, out int y)
        {
            x = 0;
            y = 0;

            int rx;
            int ry;
            int t = d;
            
            for (int s = 1; s < n; s *= 2)
            {
                rx = 1 & (t / 2);
                ry = 1 & (t ^ rx);

                rot(s, ref x, ref y, rx, ry);

                x += s * rx;
                y += s * ry;
                t /= 4;
            }
        }
        private static void rot(int n, ref int x, ref int y, int rx, int ry)
        {
            if (ry == 0)
            {
                if (rx == 1)
                {
                    x = n - 1 - x;
                    y = n - 1 - y;
                }

                int t = x;
                x = y;
                y = t;
            }
        }
    }
}
