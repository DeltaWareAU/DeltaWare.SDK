namespace DeltaWare.SDK.UI.Console.Rendering
{
    public interface IRenderer
    {
        void Render(char value, Direction direction = Direction.Right);
        void Render(string value, Direction direction = Direction.Right);

        void Translate(int x, int y);

        void TranslateX(int x);

        void TranslateY(int y);
    }
}
