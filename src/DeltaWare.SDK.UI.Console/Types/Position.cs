namespace DeltaWare.SDK.UI.Console.Types
{
    public interface IPosition
    {
        public bool IsAbsolute { get; }

        int X { get; }

        int Y { get; }
    }

    public class Position: IPosition
    {
        public bool IsAbsolute { get; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
