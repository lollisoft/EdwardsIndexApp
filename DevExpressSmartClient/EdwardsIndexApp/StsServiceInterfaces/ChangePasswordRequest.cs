using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StsServiceInterfaces
{
    [DataContract]
    public class ChangePasswordRequest
    {
        [DataMember]
        public string UserId { get; set; } 

        [DataMember]
        public string OldPassword { get; set; } 

        [DataMember]
        public string NewPassword { get; set; } 
    }
}
