using DeltaWare.SDK.UI.Console.Elements.Rendering;
using DeltaWare.SDK.UI.Console.Rendering;
using DeltaWare.SDK.UI.Console.Scaffolding;
using DeltaWare.SDK.UI.Console.Types;

namespace DeltaWare.SDK.UI.Console.Elements
{
    public abstract class ElementBase
    {
        private IScaffold _scaffold;

        internal IDimensions Dimensions => GetDimensions();

        internal void InternalRender(IRenderer renderer)
        {
            Render(new ElementRenderer(renderer, _scaffold));
        }

        internal void AttachScaffold(IScaffold scaffold)
        {
            _scaffold = scaffold;
        }

        protected abstract void Render(IRenderer renderer);

        protected abstract IDimensions GetDimensions();
    }
}
