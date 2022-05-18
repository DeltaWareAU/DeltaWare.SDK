using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace DeltaWare.SDK.UI.Console.Types
{
    public interface IDimensions
    {
        int MaxWidth { get; }
        int MinWidth { get; }
        int MaxHeight { get; }
        int MinHeight { get; }
    }

    public class Dimensions : IDimensions
    {
        public Dimensions()
        {
        }

        public Dimensions(int width, int height)
        {
            MinWidth = width;
            MinHeight = height;
        }

        public int MaxWidth { get; set; }
        public int MinWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MinHeight { get; set; }
    }

    public static class DimensionsExtensions
    {
        public static IDimensions Add(this IDimensions dimensions, int value)
        {
            return new Dimensions(dimensions.MinWidth + value, dimensions.MinHeight + value);
        }
    }
}
