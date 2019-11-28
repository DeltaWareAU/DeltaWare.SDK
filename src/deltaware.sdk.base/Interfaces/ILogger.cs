using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaWare.SDK.Base.Enums;

namespace DeltaWare.SDK.Base.Interfaces
{
    public interface ILogger
    {
        void AddLog(ILogTemplate logTemplate);
    }
}
