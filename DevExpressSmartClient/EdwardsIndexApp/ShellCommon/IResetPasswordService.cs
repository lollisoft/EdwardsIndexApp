using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShellCommon
{
    public interface IResetPasswordService
    {
        bool ResetPassword(bool isResetRequest, bool isResetComplete, string loginName, string eMail, string token, string password);
    }
}
