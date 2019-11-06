
namespace DeltaWare.SDK.Maths.Base2
{
    public class BinaryDebugView
    {
        private readonly Binary _binary;

        public BinaryDebugView(Binary binary)
        {
            _binary = binary;
        }

        public int BitLength => _binary.BitLength.Length;

        public string BinaryValue => _binary.ToString();

        public object DecimalValue => _binary.ToInt64();
    }
}
