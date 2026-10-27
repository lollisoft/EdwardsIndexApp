using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lollisoft.DatabaseLogging.ServiceInterfaces
{
    /// <summary>
    /// This service provides basic user self service functionality, such as changing the password and
    /// editing the profile data.
    /// </summary>
    [ServiceContract(Namespace = "http://www.lollisoft.de/logging/contract")]
    public interface ILoggingService
    {
        [OperationContract(Action = "LogMessageRequest", IsOneWay=true)]
        void LogMessage(LogMessageRequest request);
    }
}
