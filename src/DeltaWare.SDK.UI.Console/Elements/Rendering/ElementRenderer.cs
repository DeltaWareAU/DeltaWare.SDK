using DeltaWare.SDK.UI.Console.Elements.Scaffolds;

namespace DeltaWare.SDK.UI.Console.Elements.Rendering
{
    internal class ElementRenderer: IRenderer
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
    }
}
