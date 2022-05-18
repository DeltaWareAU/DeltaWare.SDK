using DeltaWare.SDK.UI.Console.Rendering;
using DeltaWare.SDK.UI.Console.Scaffolding;

namespace DeltaWare.SDK.UI.Console.Elements.Rendering
{
    internal class ElementRenderer : IRenderer
    {
        private readonly IRenderer _innerRenderer;

        private readonly IScaffold _scaffold;

        public ElementRenderer(IRenderer innerRenderer, IScaffold scaffold)
        {
            _innerRenderer = innerRenderer;
            _scaffold = scaffold;
        }

        public void Render(char value, Direction direction = Direction.Right)
        {
            _innerRenderer.Render(value, direction);
        }

        public void Render(string value, Direction direction = Direction.Right)
        {
            _innerRenderer.Render(value, direction);
        }

        public void Translate(int x, int y)
        {
            _innerRenderer.Translate(x, y);
        }

        public void TranslateX(int x)
        {
            _innerRenderer.TranslateX(x);
        }

        public void TranslateY(int y)
        {
            _innerRenderer.TranslateY(y);
        }
    }
}
