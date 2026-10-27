using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShellCommon
{
    public class DesignCollaboration : IDesignCollaboration
    {
        public void SetScope(string applicationName, string formName)
        {
        }

        public void SetFormDesign(byte[] xmlDesign)
        {
        }

        public void SetFormDesignImage(byte[] screenShot)
        {
        }

        public void ShareDesign()
        {
        }

        public bool IsCollaborationEnabled()
        {
            return true;
        }
    }
}
