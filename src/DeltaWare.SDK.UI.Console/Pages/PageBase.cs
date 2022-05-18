using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Pages.Builder;
using DeltaWare.SDK.UI.Console.Scaffolding;
using System;
using DeltaWare.SDK.UI.Console.Rendering;

namespace DeltaWare.SDK.UI.Console.Pages
{
    public abstract class PageBase
    {
        private Scaffold _scaffolding;

        private ConsoleUIManager _uiManager;

        internal void InternalOnBuild(ConsoleUIManager uiManager)
        {
            _uiManager = uiManager;
            _scaffolding.SetSize(_uiManager.Dimensions);

            PageBuilder builder = new PageBuilder(_scaffolding);

            OnBuild(builder);
        }

        internal void InternalRender(IRenderer renderer)
        {
            _scaffolding.Render(renderer);
        }

        protected abstract void OnBuild(IPageBuilder builder);
    }
}
