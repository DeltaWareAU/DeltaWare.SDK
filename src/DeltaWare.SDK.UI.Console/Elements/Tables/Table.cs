using System.Collections.Generic;
using DeltaWare.SDK.Serialization.Types;

namespace DeltaWare.SDK.UI.Console.Elements.Tables
{
    public abstract class Table: ElementBase
    {
        private readonly IObjectSerializer _serializer;

        public bool CanEdit { get; set; }

        protected Table(IObjectSerializer serializer)
        {
            _serializer = serializer;
        }
    }

    public class Table<T>: Table
    {
        public IList<T> Rows { get; set; } = new List<T>();

        public Table(IObjectSerializer serializer) : base(serializer)
        {
        }

        protected override void Render(IRenderer renderer)
        {
            throw new System.NotImplementedException();
        }
    }
}
