using System;

namespace DeltaWare.SDK.UI.Console.Rendering
{
    public class ConsoleRenderer: IRenderer
    {
        private int _cursorX;

        private int _cursorY;

        public ConsoleRenderer()
        {
            System.Console.BackgroundColor = ConsoleColor.Blue;
            System.Console.Clear();
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

        public void Render(string value, Direction direction = Direction.Right)
        {
            System.Console.BackgroundColor = ConsoleColor.Red;
            System.Console.Write(value);

            UpdateCursor();
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

        private void UpdateCursor()
        {
            _cursorX = System.Console.CursorLeft;
            _cursorY = System.Console.CursorTop;
        }

        public void Translate(int x, int y)
        {
            _cursorX += x;
            _cursorY += y;

            ResetCursor();
        }

        public void TranslateX(int x)
        {
            _cursorX += x;

            ResetCursor();
        }

        public void TranslateY(int y)
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
