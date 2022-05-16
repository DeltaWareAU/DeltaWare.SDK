using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Elements.Scaffolds;

namespace DeltaWare.SDK.UI.Console.Pages.Builder
{
    internal class PageBuilder: IPageBuilder
    {
        private readonly Scaffolding _scaffolding;

        public PageBuilder(Scaffolding scaffolding)
        {
            _scaffolding = scaffolding;
        }

        public ElementBase[] BuildElements()
        {
            throw new NotImplementedException();
        }

        public void AddElement<TElement>(Action<TElement> builder) where TElement : ElementBase
        {
            throw new NotImplementedException();
        }
    }
}
