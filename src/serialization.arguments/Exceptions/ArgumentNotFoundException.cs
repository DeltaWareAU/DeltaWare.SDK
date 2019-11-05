using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Arguments.Exceptions
{
    public sealed class ArgumentNotFoundException : Exception
    {
        public ArgumentNotFoundException()
        {

        }

        public ArgumentNotFoundException(string message) : base(message)
        {

        }

        public ArgumentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
