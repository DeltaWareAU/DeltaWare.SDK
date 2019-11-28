using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaWare.SDK.Common.Enums;

namespace DeltaWare.SDK.Common.Interfaces
{
    public interface ILogTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        LogCategory Category { get; }

        string Summary { get; }

        string Message { get; }

        DateTime TimeStamp { get; }
    }
}
