using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StsServiceInterfaces
{
    [DataContract]
    public class ResetPasswordRequest
    {
        [DataMember]
        public bool IsResetRequest { get; set; }
	
        [DataMember]
        public bool IsResetComplete { get; set; }
	
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string EMailAddress { get; set; }

        [DataMember]
        public string NewPassword { get; set; }
		
		[DataMember]
		public string ResetPasswordToken { get; set; }
    }
}
