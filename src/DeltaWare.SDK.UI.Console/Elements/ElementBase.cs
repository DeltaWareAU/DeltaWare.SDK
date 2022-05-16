using DeltaWare.SDK.UI.Console.Elements.Rendering;
using DeltaWare.SDK.UI.Console.Elements.Scaffolds;

namespace DeltaWare.SDK.UI.Console.Elements
{
    public abstract class ElementBase
    {
        private IScaffold _scaffold;

        internal void InternalRender(IRenderer renderer)
        {
            Render(new ElementRenderer(renderer, scaffold));
        }

        internal void AttachScaffold(IScaffold scaffold)
        {
            _scaffold = scaffold;
        }

        protected abstract void Render(IRenderer renderer);
    }
}
