namespace DeltaWare.SDK.UI.Console
{
    public interface IRenderer
    {
        void Render(char value, Direction direction = Direction.Right);
    }
}
