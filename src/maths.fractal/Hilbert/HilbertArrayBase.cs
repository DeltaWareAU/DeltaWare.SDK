using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Maths.Fractal.Hilbert
{
    public abstract class HilbertArrayBase
    {
        public int Depth { get; protected set; }

        public long Length { get; protected set; }

        public HilbertVector<T> GenerateVector<T>(T value, long index)
        {
            GetIndexCoordinates(Length, index, out long x, out long y);

            return new HilbertVector<T>(value, (int)x, (int)y, index);
        }

        public Task<HilbertVector<T>> GenerateVectorAsync<T>(T value, long index)
        {
            GetIndexCoordinates(Length, index, out long x, out long y);

            return Task.FromResult(new HilbertVector<T>(value, (int) x, (int) y, index));
        }

        protected static void GetIndexCoordinates(long length, long currentIndex, out long x, out long y)
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
