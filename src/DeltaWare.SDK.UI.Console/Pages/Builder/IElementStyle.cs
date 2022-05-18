using System;
using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Scaffolding;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    public interface IElementStyle<out TElement> : IElementConfiguration<TElement> where TElement : ElementBase, new()
    {
        IElementConfiguration<TElement> SetStyle(Action<IScaffoldConfiguration> style);
    }
}
