namespace DeltaWare.SDK.UI.Console.Types
{
    public interface IPadding
    {
        int Top { get; }

        int Left { get; }

        int Bottom { get; }

        int Right { get; }
    }

    public class Padding: IPadding
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public int Bottom { get; set; }

        public int Right { get; set; }
    }
}
