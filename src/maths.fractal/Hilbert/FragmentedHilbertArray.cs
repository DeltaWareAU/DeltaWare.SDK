using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    public class FragmentedHilbertArray : HilbertArrayBase, IDisposable
    {
        private const int FragmentSize = 1024;

        private long _currentIndex;

        private readonly StreamReader _stream;

        public readonly long Depth;

        public readonly long Length;

        public bool EndOfStream { get; private set; }

        public bool Disposed { get; private set; }

        public FragmentedHilbertArray(StreamReader stream)
        {
            _stream = stream;

            Length = stream.BaseStream.Length;

            int depth = 1;

            while (Length > Math.Pow(4, depth))
            {
                depth++;
            }

            Depth = depth;
        }

        public HilbertVector<char>[] NextFragment()
        {   
            List<HilbertVector<char>> vectors = new List<HilbertVector<char>>();

            for (int i = 0; i < FragmentSize; i++)
            {
                if (_stream.EndOfStream)
                {
                    EndOfStream = true;

                    return vectors.ToArray();
                }

                GetIndexCoordinates(Length, _currentIndex++, out long x, out long y);

                vectors.Add(new HilbertVector<char>((char)_stream.Read(), (int)x, (int)y, _currentIndex));
            }

            return vectors.ToArray();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                _stream.Dispose();
            }

            Disposed = true;
        }
    }
}
