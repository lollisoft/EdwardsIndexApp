using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lollisoft.Common.Logging.Interfaces
{
    /// <summary>
    /// The DatabaseListener will use a configured IDatabaseTracer instance when it is configured by a static member databaseTarget.
    /// If the databaseTarget is not set, the tracelistener will do nothing.
    /// </summary>
    public interface IDatabaseTracer
    {
        void TraceMessage(string message);

        void TraceMessage(string message, TraceEventType eventType);
    }
}
