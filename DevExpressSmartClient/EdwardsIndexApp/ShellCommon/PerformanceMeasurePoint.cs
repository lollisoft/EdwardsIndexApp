using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace ShellCommon
{
    public class PerformanceMeasurePoint
    {
        public Guid CorrelationId
        {
            get;
            set;
        }

        public TimeSpan OperationStart
        {
            get;
            set;
        }

        public TimeSpan OperationEnd
        {
            get;
            set;
        }

        public TimeSpan OperationDuration
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public string ServiceEndpoint
        {
            get;
            set;
        }

        public string OperationName
        {
            get;
            set;
        }

    }
}
