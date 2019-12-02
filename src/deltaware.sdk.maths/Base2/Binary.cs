
using System;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace DeltaWare.SDK.Maths.Base2
{
    [DebuggerTypeProxy(typeof(BinaryDebugView))]
    public partial struct Binary
    {
        private bool _valueGenerated;

        private readonly byte[] _value;
        
        private readonly long _longValue;

        public byte[] Value
        {
            get
            {
                if (_valueGenerated)
                {
                    return _value;
                }

                byte[] tempBytes = BitConverter.GetBytes(this);

                for (int i = 0; i < tempBytes.Length; i++)
                {
                    _value[i] = tempBytes[i];
                }

                _valueGenerated = true;

                return _value;
            }
        }

        public BinaryLength Length { get; }
        
        public static Binary Zero = new Binary(0, 1);

        public static Binary One = new Binary(1, 1);

        public Binary(long longValue, int length)
        {
            Length = new BinaryLength(length);
            _longValue = longValue;

            _value = new byte[Length];
            _valueGenerated = false;
        }
        
        internal Binary(long longValue, BinaryLength length)
        {
            Length = length;
            _longValue = longValue;

            _value = new byte[Length];
            _valueGenerated = false;
        }

        public new string ToString()
        {
            string value = Convert.ToString(this, 2);

            if (value.Length > Length)
            {
                value = value.Substring(Length);
            }
            else if (value.Length < Length)
            {
                value = new string('0', Length - value.Length) + value;
            }

            return value;
        }
    }
}
