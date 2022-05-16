using System.Collections.Generic;
using System.Numerics;

namespace DeltaWare.SDK.UI.Console.Elements.Scaffolds
{
    public interface IScaffold
    {
        Vector2 Size { get; }

        IScaffold Attach(ElementBase element);
    }

    internal class Scaffold : IScaffold
    {
        private readonly ElementBase _attachedElement;

        private readonly List<Scaffold> _children = new();

        public Vector2 Size { get; set; }

        public Scaffold(ElementBase element)
        {
            _attachedElement = element;
            _attachedElement.AttachScaffold(this);
        }

        public IScaffold Attach(ElementBase element)
        {
            Scaffold scaffold = new Scaffold(element);

            _children.Add(scaffold);

            return scaffold;
        }

        public void Render(IRenderer renderer)
        {
            _attachedElement.InternalRender(renderer);

            foreach (Scaffold child in _children)
            {
                child.Render(renderer);
            }
        }
    }
}
