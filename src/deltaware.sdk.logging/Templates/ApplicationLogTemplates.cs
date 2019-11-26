
using System;

using DeltaWare.SDK.Core.Enums;
using DeltaWare.SDK.Core.Interfaces;

namespace DeltaWare.SDK.Logging.Templates
{
    public class ApplicationLogTemplates : ILogTemplate
    {
        public LogCategory Category { get; }

        public string Summary { get; }

        public string Message { get; }

        public DateTime TimeStamp { get; }

        private ApplicationLogTemplates(LogCategory category, string summary)
        {
            Category = category;
            Summary = summary;
            Message = string.Empty;
            TimeStamp = DateTime.UtcNow;
        }

        private ApplicationLogTemplates(LogCategory category, string summary, string message)
        {
            Category = category;
            Summary = summary;
            Message = message;
            TimeStamp = DateTime.UtcNow;
        }

        public static ILogTemplate AppStartup()
        {
            return new ApplicationLogTemplates(LogCategory.Status, "Application Startup");
        }
    }
}
