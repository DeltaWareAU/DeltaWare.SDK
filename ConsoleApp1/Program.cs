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
            string filePath = @"C:\Projects\Final\hs_alt_HuRef_chr1.fa";

            List<char> characters= new List<char>();

            using (StreamReader stream = new StreamReader(filePath))
            {
                do
                {
                    characters.Add((char)stream.Read());
                } while (!stream.EndOfStream);
            }

            HilbertArray<char> hilbertCharacters = HilbertArray<char>.Generate(characters.ToArray());

            int resolution = (int) Math.Pow(2, hilbertCharacters.Depth);

            using (Bitmap bitmap = new Bitmap(resolution,resolution))
            {
                Color color;
                HilbertVector<char> vector;

                for (int i = 0; i < hilbertCharacters.Length; i++)
                {
                    vector = hilbertCharacters[i];

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

                bitmap.Save(@"C:\Users\Bradl\OneDrive\Pictures\HilbertColors.png", ImageFormat.Png);
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
