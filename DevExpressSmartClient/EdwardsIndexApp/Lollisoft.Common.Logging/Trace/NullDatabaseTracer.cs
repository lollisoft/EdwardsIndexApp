using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lollisoft.Common.Logging.Interfaces;

namespace Lollisoft.Common.Logging.Trace
{
    public class NullDatabaseTracer : IDatabaseTracer
    {
        public void TraceMessage(string message)
        {
            
        }

        public void TraceMessage(string message, TraceEventType eventType)
        {

        }
    }
}
