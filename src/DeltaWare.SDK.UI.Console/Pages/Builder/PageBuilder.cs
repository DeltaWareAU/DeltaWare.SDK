using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Scaffolding;
using System;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    internal class PageBuilder : IPageBuilder
    {
        private readonly Scaffold _scaffold;

        public PageBuilder(Scaffold scaffold)
        {
            _scaffold = scaffold;
        }

        public IElementBuilder<TElement> AddElement<TElement>() where TElement : ElementBase, new()
        {
            return new ElementBuilder<TElement>(_scaffold);
        }
    }
}
