using DeltaWare.SDK.UI.Console.Elements;

namespace DeltaWare.SDK.UI.Console.Scaffolding
{
    public interface IScaffoldBuilder : IScaffoldConfiguration
    {
        IScaffoldBuilder Attach(ElementBase element);
    }
}
