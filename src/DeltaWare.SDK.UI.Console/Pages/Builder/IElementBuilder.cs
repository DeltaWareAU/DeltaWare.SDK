using DeltaWare.SDK.UI.Console.Elements;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    public interface IElementBuilder<out TElement> : IElementStyle<TElement> where TElement : ElementBase, new()
    {
    }
}
