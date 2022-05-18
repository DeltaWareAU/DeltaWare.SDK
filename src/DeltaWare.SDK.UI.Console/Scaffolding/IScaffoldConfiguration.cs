using DeltaWare.SDK.UI.Console.Types;

namespace DeltaWare.SDK.UI.Console.Scaffolding
{
    public interface IScaffoldConfiguration : IScaffold
    {
        void SetSize(int width, int height);
        void SetSize(IDimensions dimensions);

        void SetPosition(int x, int y);

        void SetMargin(int margin);
        void SetMargin(int marginHorizontal, int marginVertical);
        void SetMargin(int marginTop, int marginRight, int marginBottom, int marginLeft);
        
        void SetPadding(int padding);
        void SetPadding(int paddingHorizontal, int paddingVertical);
        void SetPadding(int paddingTop, int paddingRight, int paddingBottom, int paddingLeft);
    }
}
