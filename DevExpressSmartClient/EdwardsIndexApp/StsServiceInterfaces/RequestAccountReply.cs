using System.Runtime.Serialization;

namespace StsServiceInterfaces
{
    [DataContract]
    public class RequestAccountReply
    {
        [DataMember]
        public bool AccountRequested { get; set; }
    }
}
