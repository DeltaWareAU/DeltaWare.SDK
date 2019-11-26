using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaWare.SDK.Core.Enums;

namespace DeltaWare.SDK.Core.Interfaces
{
    public interface ILogger
    {
        void AddLog(ILogTemplate logTemplate);

        void LogInformation(string message);
    }
}
