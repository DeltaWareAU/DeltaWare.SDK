using DeltaWare.SDK.UI.Console.Elements;
using System;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    public interface IElementConfiguration<out TElement> where TElement : ElementBase, new()
    {
        void Initialize(Action<TElement> configuration);
    }
}
