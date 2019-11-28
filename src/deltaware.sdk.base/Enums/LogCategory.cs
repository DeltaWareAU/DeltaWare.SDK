using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Base.Enums
{
    /// <summary>
    /// Log Category.
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// Used on logs that are related to the state, useful for debugging.
        /// </summary>
        Debug,
        /// <summary>
        /// Used on logs that are related to the status.
        /// </summary>
        Status,
        /// <summary>
        /// Used on logs that are information related but are not status related.
        /// </summary>
        Info,
        /// <summary>
        /// Used on logs as a low priority alert.
        /// </summary>
        Warning,
        /// <summary>
        /// Used on logs as a high priority warning.
        /// </summary>
        Alert,
        /// <summary>
        /// Used on logs where an exception has occured but it was not fatal.
        /// </summary>
        Error,
        /// <summary>
        /// Used on logs where an exception has occured but it was fatal.
        /// </summary>
        Fatal
    }
}
