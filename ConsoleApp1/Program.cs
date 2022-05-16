using System;
using DeltaWare.SDK.UI.Console;
using DeltaWare.SDK.UI.Console.Elements;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleRenderer renderer = new ConsoleRenderer();


            Border border = new Border(renderer);
            
            border.Width = 20;
            border.Height = 15;

            border.Render();

            //renderer.Render('X');
            //renderer.Render('X');
            //renderer.Render('X');
            //renderer.Render('X');
            //renderer.Render('Y', Direction.Down);
            //renderer.Render('Y', Direction.Down);
            //renderer.Render('Y', Direction.Down);
            //renderer.Render('Y', Direction.Down);
            //renderer.Render('X', Direction.Left);
            //renderer.Render('X', Direction.Left);
            //renderer.Render('X', Direction.Left);

            Console.Read();
        }
    }
}
