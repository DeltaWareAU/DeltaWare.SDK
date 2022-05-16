using System;
using DeltaWare.SDK.UI.Console.Elements;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    public interface IPageBuilder
    {
        void AddElement<TElement>(Action<TElement> builder) where TElement : ElementBase;
    }
}
