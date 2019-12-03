
namespace DeltaWare.SDK.Maths.Types
{
    /// <summary>
    /// A Point at which the Value of a Function is Largest (a <see cref="Maxima"/>) or Smallest (a <see cref="Minima"/>).
    /// </summary>
    public struct Extrema
    {
        /// <summary>
        /// The Highest Possible Value.
        /// </summary>
        public int Maxima { get; }

        /// <summary>
        /// The Lowest Possible Value.
        /// </summary>
        public int Minima { get; }

        public Extrema(int min, int max)
        {
            Maxima = max;
            Minima = min;
        }
    }
}
