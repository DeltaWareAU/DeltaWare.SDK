using System;
using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Elements.Scaffolds;
using DeltaWare.SDK.UI.Console.Pages.Builder;

namespace DeltaWare.SDK.UI.Console.Pages
{
    public abstract class PageBase
    {
        private readonly Scaffolding _scaffolding = new Scaffolding();

        private ElementBase[] _elements;

        internal void InternalOnBuild()
        {
            PageBuilder builder = new PageBuilder(_scaffolding);

            OnBuild(builder);

            _elements = builder.BuildElements();
        }

        internal void InternalRender(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        protected abstract void OnBuild(IPageBuilder builder);
    }
}
