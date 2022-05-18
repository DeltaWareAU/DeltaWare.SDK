using DeltaWare.Dependencies;
using DeltaWare.Dependencies.Abstractions;
using DeltaWare.SDK.UI.Console.Pages;
using DeltaWare.SDK.UI.Console.Rendering;
using DeltaWare.SDK.UI.Console.Types;

namespace DeltaWare.SDK.UI.Console
{
    public class ConsoleUIManager
    {
        public IDependencyCollection Dependencies { get; } = new DependencyCollection();

        internal Dimensions Dimensions => new Dimensions(System.Console.WindowWidth, System.Console.WindowHeight);

        public void AddPage<TPage>() where TPage : PageBase
        {
            Dependencies.AddScoped<TPage>();
        }

        public void Render<TPage>() where TPage : PageBase
        {
            ConsoleRenderer renderer = new ConsoleRenderer();

            using IDependencyProvider provider = Dependencies.BuildProvider();

            TPage page = provider.GetDependency<TPage>();
            
            page.InternalOnBuild(this);
            page.InternalRender(renderer);
        }
    }
}
