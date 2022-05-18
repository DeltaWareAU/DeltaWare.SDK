using DeltaWare.SDK.UI.Console.Rendering;
using DeltaWare.SDK.UI.Console.Types;

namespace DeltaWare.SDK.UI.Console.Elements
{
    public class TextField : ElementBase
    {
        public string Text { get; set; }

        protected override void Render(IRenderer renderer)
        {
            renderer.Render(Text);
        }

        protected override IDimensions GetDimensions()
        {
            return new Dimensions(Text.Length, 1);
        }
    }
}
