using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShellCommon
{
    public interface IRequestAccountService
    {
        bool RequestAccount(string userId, string eMail, string applicationId);
    }
}
