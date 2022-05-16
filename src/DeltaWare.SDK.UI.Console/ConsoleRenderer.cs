using System;

namespace DeltaWare.SDK.UI.Console
{
    public class ConsoleRenderer: IRenderer
    {
        private int _cursorX;

        private int _cursorY;

        public ConsoleRenderer()
        {
            System.Console.CursorVisible = false;
        }

        public void Render(char value, Direction direction = Direction.Right)
        {
            switch (direction)
            {
                case Direction.Up:
                    TranslateY(-1);
                    break;
                case Direction.Down:
                    TranslateY(1);
                    break;
                case Direction.Left:
                    TranslateX(-1);
                    break;
                case Direction.Right:
                    TranslateX(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            System.Console.Write(value);
        }

        private void ResetCursor()
        {
            if (_cursorX != System.Console.CursorLeft)
            {
                System.Console.CursorLeft = _cursorX;
            }

            if (_cursorY != System.Console.CursorTop)
            {
                System.Console.CursorTop = _cursorY;
            }
        }

        private void Translate(int x, int y)
        {
            _cursorX += x;
            _cursorY += y;

            ResetCursor();
        }

        private void TranslateX(int x)
        {
            _cursorX += x;

            ResetCursor();
        }

        private void TranslateY(int y)
        {
            _cursorY += y;

            ResetCursor();
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
