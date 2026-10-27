using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StsServiceInterfaces
{
    [DataContract]
    public class RequestAccountRequest
    {
        [DataMember]
        public string UserId { get; set; } 

        [DataMember]
        public string ApplicationId { get; set; } 

        [DataMember]
        public string EMailAddress { get; set; } 
    }
}
