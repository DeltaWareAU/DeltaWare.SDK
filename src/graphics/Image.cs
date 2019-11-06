using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DeltaWare.SDK.Drawing
{
    public class Image : IDisposable
    {
        public System.Drawing.Bitmap Bitmap { get; }

        public Int32[] Bits { get; }

        public bool Disposed { get; private set; }

        public int Height { get; }

        public int Width { get; }

        protected GCHandle BitsHandle { get; }

        public Image(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new System.Drawing.Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public Image(int size) : this(size, size)
        {
        }

        public void SetPixel(int x, int y, Color color)
        {
            int index = x + (y * Width);
            int col = color.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];

            Color result = Color.FromArgb(col);

            return result;
        }

        public void Save(string fileName, ImageFormat imageFormat)
        {
            Bitmap.Save(fileName, imageFormat);
        }

        public void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            Bitmap.Dispose();

            BitsHandle.Free();
        }
    }
}
