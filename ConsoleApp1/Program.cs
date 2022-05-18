using System;
using DeltaWare.SDK.UI.Console;
using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Pages;
using DeltaWare.SDK.UI.Console.Pages.Builder;
using DeltaWare.SDK.UI.Console.Rendering;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUIManager manager = new ConsoleUIManager();

            manager.AddPage<TestPage>();
            manager.Render<TestPage>();

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

    public class TestPage: PageBase
    {
        protected override void OnBuild(IPageBuilder builder)
        {
            builder.AddElement<TextField>()
                .Initialize(t => t.Text = "HELLO!");
        }
    }
}
