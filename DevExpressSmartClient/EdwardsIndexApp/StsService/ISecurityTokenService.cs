using StsService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace StsService
{
    /// <summary>
    /// WCF contract for a Secure Token Service
    /// </summary>
    [ServiceContract]
    public interface ISecurityTokenService
    {
        [OperationContract(Action = Constants.Trust.Actions.Issue, ReplyAction = Constants.Trust.Actions.IssueReply)]
        Message IssueToken(Message rstMessage);
    }
}
