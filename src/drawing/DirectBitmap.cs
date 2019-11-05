using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DeltaWare.SDK.Drawing
{
    public class Bitmap : IDisposable
    {
        public System.Drawing.Bitmap InnerBitmap { get; }

        public int[] Bits { get; }

        public bool Disposed { get; private set; }

        public int Height { get; }

        public int Width { get; }
        
        protected GCHandle BitsHandle { get; }

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            InnerBitmap = new System.Drawing.Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
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
            InnerBitmap.Save(fileName, imageFormat);
        }

        public void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            InnerBitmap.Dispose();

            BitsHandle.Free();
        }
    }
}
