using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.UI.Console.Types
{
    public interface IMargin
    {
        int Top { get; }

        int Left { get; }

        int Bottom { get; }

        int Right { get; }
    }

    public class Margin: IMargin
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public int Bottom { get; set; }

        public int Right { get; set; }
    }
}
