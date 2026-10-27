using System.Runtime.Serialization;

namespace StsServiceInterfaces
{
    [DataContract]
    public class ResetPasswordReply
    {
        [DataMember]
        public bool PasswordReset { get; set; }
    }
}
