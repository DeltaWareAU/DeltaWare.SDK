using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    internal sealed class HilbertArrayBuilder<T>
    {
        private List<HilbertVector<T>> _verctors;

        public HilbertArrayBuilder()
        {
            _verctors = new List<HilbertVector<T>>();
        }

        public HilbertArrayBuilder(int startingCapacity)
        {
            _verctors = new List<HilbertVector<T>>(startingCapacity);

        }

        public void AddItem(T item)
        {

        }
    }
}
