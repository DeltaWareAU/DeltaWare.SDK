namespace DeltaWare.SDK.Console
{
    public static class ConsoleLoadingBar
    {
        private const char LoadedChar = '▓';

        private const char HalfLoadedChar = '▒';

        private const char BlankingChar = '░';

        private static void RenderLoadingBar(int index, int length, int charWidth)
        {
            double percentage = (100f * index + 1) / length;

            int loaded;

            bool half = false;

            string loadingBar;

            if (index + 1 == length)
            {
                loadingBar = new string(LoadedChar, charWidth);
            }
            else
            {
                if (percentage == 0)
                {
                    loaded = 0;
                }
                else
                {
                    double lengthPercentage = charWidth / 100f;
                    double currentLength = lengthPercentage * percentage;

                    var temp = currentLength - Math.Truncate(currentLength);

                    half = temp > 0.5;

                    loaded = (int)(currentLength);
                }

                loadingBar = new string(LoadedChar, loaded);

                if (half && loadingBar.Length != charWidth)
                {
                    loadingBar += HalfLoadedChar;
                }

                loadingBar = loadingBar.PadRight(charWidth, BlankingChar);
            }

            System.Console.OutputEncoding = System.Text.Encoding.Default;
            System.Console.Write($"{loadingBar}\r");
        }

        public static void Render<T>(int charWidth, T[] values, Action<T> action)
        {
            int length = values.Length;

            for (int i = 0; i < length; i++)
            {
                action.Invoke(values[i]);

                RenderLoadingBar(i, length, charWidth);
            }
        }

        public static void Render(int charWidth, int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action.Invoke();

                RenderLoadingBar(i, count, charWidth);
            }
        }
    }
}