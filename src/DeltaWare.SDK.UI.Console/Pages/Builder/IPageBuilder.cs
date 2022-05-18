using System;
using DeltaWare.SDK.UI.Console.Elements;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    public interface IPageBuilder
    {
        IElementBuilder<TElement> AddElement<TElement>() where TElement : ElementBase, new();
    }
}
