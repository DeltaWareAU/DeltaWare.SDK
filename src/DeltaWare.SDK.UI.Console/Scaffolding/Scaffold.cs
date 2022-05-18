using DeltaWare.SDK.UI.Console.Elements;
using DeltaWare.SDK.UI.Console.Types;
using System.Collections.Generic;
using DeltaWare.SDK.UI.Console.Rendering;

namespace DeltaWare.SDK.UI.Console.Scaffolding
{
    internal class Scaffold : IScaffoldBuilder
    {
        private readonly ElementBase _attachedElement;

        private readonly Scaffold _parent;

        private readonly List<Scaffold> _children = new();

        private IDimensions _dimensions = null;
        private IMargin _margin = new Margin();
        private IPadding _padding = new Padding();
        private IPosition _position = new Position();

        public bool Outlined { get; set; } = true;

        public IDimensions Dimensions
        {
            get
            {
                if (_dimensions == null)
                {
                    return _attachedElement?.Dimensions.Add(3);
                }

                return _dimensions;
            }
        }
        public IMargin Margin { get; }
        public IPadding Padding { get; }
        public IPosition Position { get; }

        public Scaffold()
        {
        }

        private Scaffold(ElementBase element, Scaffold parent)
        {
            _attachedElement = element;
            _attachedElement.AttachScaffold(this);
            _parent = parent;
        }

        public IScaffoldBuilder Attach(ElementBase element)
        {
            Scaffold scaffold = new Scaffold(element, this);

            _children.Add(scaffold);

            return scaffold;
        }

        public void Render(IRenderer renderer)
        {
            if (Outlined)
            {
                RenderOutline(renderer);
            }

            _attachedElement?.InternalRender(renderer);

            foreach (Scaffold child in _children)
            {
                child.Render(renderer);
            }
        }

        private void RenderOutline(IRenderer renderer)
        {
            renderer.Render('╔');

            for (int i = 0; i < Dimensions.MinWidth - 2; i++)
            {
                renderer.Render('═');
            }

            renderer.Render('╗');

            for (int i = 0; i < Dimensions.MinHeight - 2; i++)
            {
                renderer.Render('║', Direction.Down);
            }

            renderer.Render('╝', Direction.Down);

            for (int i = 0; i < Dimensions.MinWidth - 2; i++)
            {
                renderer.Render('═', Direction.Left);
            }

            renderer.Render('╚', Direction.Left);

            for (int i = 0; i < Dimensions.MinHeight - 2; i++)
            {
                renderer.Render('║', Direction.Up);
            }

            renderer.TranslateX(1);
        }

        public void SetSize(int width, int height)
        {
            throw new System.NotImplementedException();
        }

        public void SetSize(IDimensions dimensions)
        {
            throw new System.NotImplementedException();
        }

        public void SetPosition(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void SetMargin(int margin)
        {
            throw new System.NotImplementedException();
        }

        public void SetMargin(int marginHorizontal, int marginVertical)
        {
            throw new System.NotImplementedException();
        }

        public void SetMargin(int marginTop, int marginRight, int marginBottom, int marginLeft)
        {
            throw new System.NotImplementedException();
        }

        public void SetPadding(int padding)
        {
            throw new System.NotImplementedException();
        }

        public void SetPadding(int paddingHorizontal, int paddingVertical)
        {
            throw new System.NotImplementedException();
        }

        public void SetPadding(int paddingTop, int paddingRight, int paddingBottom, int paddingLeft)
        {
            throw new System.NotImplementedException();
        }
    }
}
