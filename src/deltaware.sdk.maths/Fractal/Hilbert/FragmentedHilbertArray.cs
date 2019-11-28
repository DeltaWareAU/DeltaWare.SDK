
using System;
using System.Collections.Generic;
using System.IO;

using DeltaWare.SDK.Common.Types;

namespace DeltaWare.SDK.Maths.Fractal.Hilbert
{
    public class FragmentedHilbertArray : HilbertArrayBase, IDisposable
    {
        private int _fragmentSize;

        private int _currentIndex = 0;

        private readonly StreamReader _stream;

        public bool EndOfStream { get; private set; }

        public bool Disposed { get; private set; }

        public FragmentedHilbertArray(StreamReader stream, int fragmentSize = 4096)
        {
            _stream = stream;
            _fragmentSize = fragmentSize;

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

            for (int i = 0; i < _fragmentSize; i++)
            {
                if (_stream.EndOfStream)
                {
                    EndOfStream = true;

                    return vectors.ToArray();
                }

                GetIndexCoordinates(Length, _currentIndex++, out LongCoordinate coordinates);

                vectors.Add(new HilbertVector<char>((char)_stream.Read(), coordinates.ToCoordinate(), _currentIndex));
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
