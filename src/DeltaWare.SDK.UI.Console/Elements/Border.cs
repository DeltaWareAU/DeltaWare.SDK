
namespace DeltaWare.SDK.UI.Console.Elements
{
    public class Border
    {
        private readonly char _horizontalLine = '═';
        private readonly char _verticalLine = '║';

        private readonly char _topLeft = '╔';
        private readonly char _topRight = '╗';
        private readonly char _bottomLeft = '╚';
        private readonly char _bottomRight = '╝';

        private readonly IRenderer _renderer;

        public int Width { get; set; }

        public int Height { get; set; }

        public Border(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public void Render()
        {
            _renderer.Render(_topLeft);

            for (int i = 0; i < Width - 2; i++)
            {
                _renderer.Render(_horizontalLine);
            }

            _renderer.Render(_topRight);

            for (int i = 0; i < Height - 2; i++)
            {
                _renderer.Render(_verticalLine, Direction.Down);
            }
            
            _renderer.Render(_bottomRight, Direction.Down);

            for (int i = 0; i < Width - 2; i++)
            {
                _renderer.Render(_horizontalLine , Direction.Left);
            }

            _renderer.Render(_bottomLeft, Direction.Left);

            for (int i = 0; i < Height - 2; i++)
            {
                _renderer.Render(_verticalLine, Direction.Up);
            }
        }
    }
}
