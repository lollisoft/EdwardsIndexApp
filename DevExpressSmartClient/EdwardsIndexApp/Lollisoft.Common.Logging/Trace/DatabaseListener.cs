using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lollisoft.Common.Logging.Trace
{
	class DatabaseListener : TraceListener
    {
        protected string _msg = "";
        protected TraceEventType _eventType;

        public override void Write(string message)
        {
            _msg += message;
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args) 
        {
			_eventType = eventType;
            base.TraceEvent(eventCache, source, eventType, id, format, args);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            _eventType = eventType;
            base.TraceEvent(eventCache, source, eventType, id);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            _eventType = eventType;
            base.TraceEvent(eventCache, source, eventType, id, message);
        }

        public override void WriteLine(string message)
        {
            DatabaseTracerFactory.GetTracer().TraceMessage(_msg + message, _eventType);
			_eventType = TraceEventType.Verbose;
			_msg = "";
        }
    }

}
