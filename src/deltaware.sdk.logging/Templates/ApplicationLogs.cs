
using System;

using DeltaWare.SDK.Base.Enums;
using DeltaWare.SDK.Base.Interfaces;

namespace DeltaWare.SDK.Logging.Templates
{
    public class ApplicationLogs : ILogTemplate
    {
        public LogCategory Category { get; }

        public string Summary { get; }

        public string Message { get; }

        public DateTime TimeStamp { get; }

        private ApplicationLogs(LogCategory category, string summary)
        {
            Category = category;
            Summary = summary;
            Message = string.Empty;
            TimeStamp = DateTime.UtcNow;
        }

        private ApplicationLogs(LogCategory category, string summary, string message)
        {
            Category = category;
            Summary = summary;
            Message = message;
            TimeStamp = DateTime.UtcNow;
        }

        public static ILogTemplate AppStarted()
        {
            return new ApplicationLogs(LogCategory.Status, "Application Startup");
        }

        public static ILogTemplate AppTerminated()
        {
            return new ApplicationLogs(LogCategory.Status, "Application Terminated");
        }
    }
}
