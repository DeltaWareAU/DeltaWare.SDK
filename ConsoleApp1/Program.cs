using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaWare.Tools.Maths.Fractal.Hilbert;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Projects\Final\hs_alt_HuRef_chr19.fa";

            using (FragmentedHilbertArray hilbertArray = new FragmentedHilbertArray(new StreamReader(filePath)))
            {
                int resolution = (int)Math.Pow(2, hilbertArray.Depth);

                using (DirectBitmap bitmap = new DirectBitmap(resolution, resolution))
                {
                    do
                    {
                        HilbertVector<char>[] vectors = hilbertArray.NextFragment();

                        for (int i = 0; i < vectors.Length; i++)
                        {
                            Color color;

                            HilbertVector<char>  vector = vectors[i];

                            switch (vector.Value)
                            {
                                case 'A':
                                    color = Color.Red;
                                    break;
                                case 'C':
                                    color = Color.Yellow;
                                    break;
                                case 'T':
                                    color = Color.Green;
                                    break;
                                case 'G':
                                    color = Color.Blue;
                                    break;
                                default:
                                    color = Color.Black;
                                    break;
                            }

                            bitmap.SetPixel((int)vector.X, (int)vector.Y, color);
                        }
                    }
                    while (!hilbertArray.EndOfStream);

                    bitmap.Save(@"C:\Projects\Final\hs_alt_HuRef_chr19.png", ImageFormat.Png);
                }
            }
        }

        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Orange;
                case 2:
                    return Color.Yellow;
                case 3:
                    return Color.Green;
                case 4:
                    return Color.Magenta;
                default: // case 5:
                    return Color.Blue;
            }

            //switch ((int)div)
            //{
            //    case 0:
            //        return Color.FromArgb(255, 255, ascending, 0);
            //    case 1:
            //        return Color.FromArgb(255, descending, 255, 0);
            //    case 2:
            //        return Color.FromArgb(255, 0, 255, ascending);
            //    case 3:
            //        return Color.FromArgb(255, 0, descending, 255);
            //    case 4:
            //        return Color.FromArgb(255, ascending, 0, 255);
            //    default: // case 5:
            //        return Color.FromArgb(255, 255, 0, descending);
            //}
        }
    }
}
