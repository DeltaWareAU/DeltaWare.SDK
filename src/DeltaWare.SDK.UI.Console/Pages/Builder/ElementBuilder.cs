using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Scaffolding;
using System;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    internal class ElementBuilder<TElement> : IElementBuilder<TElement> where TElement : ElementBase, new()
    {
        private readonly IScaffoldBuilder _scaffold;

        private readonly TElement _element;

        public ElementBuilder(IScaffoldBuilder scaffold)
        {
            _element = new TElement();

            _scaffold = scaffold.Attach(_element);
        }

        public void Initialize(Action<TElement> configuration)
        {
            configuration.Invoke(_element);
        }

        public IElementConfiguration<TElement> SetStyle(Action<IScaffoldConfiguration> style)
        {
            style.Invoke(_scaffold);

            return this;
        }
    }
}
