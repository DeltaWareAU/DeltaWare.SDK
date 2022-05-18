using DeltaWare.SDK.UI.Console.Elements;
using System.Numerics;
using DeltaWare.SDK.UI.Console.Types;

namespace DeltaWare.SDK.UI.Console.Scaffolding
{
    public interface IScaffold
    {
        IDimensions Dimensions { get; }

        IMargin Margin { get; }

        IPadding Padding { get; }

        IPosition Position { get; }
    }
}
