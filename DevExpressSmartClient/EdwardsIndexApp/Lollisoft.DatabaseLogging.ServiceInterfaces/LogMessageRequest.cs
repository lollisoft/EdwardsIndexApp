using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lollisoft.DatabaseLogging.ServiceInterfaces
{
    [DataContract(Namespace = "http://www.lollisoft.de/logging/schema")]
    public class LogMessageRequest
    {
        [DataMember]
        public int MachineId { get; set; }

        [DataMember]
        public int ApplicationId { get; set; }

        [DataMember]
        public int DeploymentEnvironmentId { get; set; }

        [DataMember]
        public List<LogMessage> Messages { get; set; }
    }
}
