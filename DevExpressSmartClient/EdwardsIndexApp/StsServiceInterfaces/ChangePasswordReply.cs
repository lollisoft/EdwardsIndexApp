using System.Runtime.Serialization;

namespace StsServiceInterfaces
{
    [DataContract]
    public class ChangePasswordReply
    {
        [DataMember]
        public bool PasswordChanged { get; set; }
    }
}
