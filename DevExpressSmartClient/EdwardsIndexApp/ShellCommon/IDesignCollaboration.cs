using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShellCommon
{
    // Generic interface to publish application designs
    public interface IDesignCollaboration
    {
        bool IsCollaborationEnabled();

        /// <summary>
        /// A user sets the scope for a design suggestion. The application scope is a link into
        /// the model behind the running application. If the user has the right to design other
        /// applications, it can be set. If not, the parameter is ignored and the feature is not
        /// active.
        /// </summary>
        /// <param name="applicationName">The application a user is testing out.</param>
        /// <param name="formName">The form a design is meant for, if present. Null otherwise.</param>
        void SetScope(string applicationName, string formName);

        /// <summary>
        /// Any XML based form design that an application may produce.
        /// </summary>
        /// <param name="xmlDesign"></param>
        void SetFormDesign(byte[] xmlDesign);

        /// <summary>
        /// To supply a screenshot, use thid function.
        /// </summary>
        /// <param name="screenShot"></param>
        void SetFormDesignImage(byte[] screenShot);

        /// <summary>
        /// Share the design to other participants collaborating at the design stage.
        /// </summary>
        void ShareDesign();
    }
}
