using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lollisoft.DatabaseLogging.ServiceInterfaces
{
    [DataContract(Namespace = "http://www.lollisoft.de/logging/schema")]
    public class LogMessage
    {
        [DataMember]
        public DateTime ClientTimestamp { get; set; }

        [DataMember]
        public TraceEventType EventType { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string IPAddress { get; set; }
	}
}
