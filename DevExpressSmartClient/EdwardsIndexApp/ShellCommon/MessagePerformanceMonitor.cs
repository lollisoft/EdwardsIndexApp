using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace ShellCommon
{
    public class MessagePerformanceMonitor : IDispatchMessageInspector, IClientMessageInspector
    {
        Stopwatch sw = new Stopwatch();

        private ConcurrentDictionary<Guid, PerformanceMeasurePoint> PerformanceMeasures = new ConcurrentDictionary<Guid, PerformanceMeasurePoint>();
   
        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref Message request, IClientChannel channel,
            InstanceContext instanceContext)
        {
            //request = TraceMessage(request.CreateBufferedCopy(int.MaxValue));
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //reply = TraceMessage(reply.CreateBufferedCopy(int.MaxValue));
        }

        #endregion

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //reply = TraceMessage(reply.CreateBufferedCopy(int.MaxValue));

            TimeSpan operationElapsed = sw.Elapsed;

            if (correlationState != null && correlationState is Guid)
            {
                Guid perfCorrelation = (Guid)correlationState;

                PerformanceMeasurePoint pm = null;

                if (PerformanceMeasures.TryGetValue(perfCorrelation, out pm))
                {
                    pm.OperationEnd = operationElapsed;
                    pm.OperationDuration = pm.OperationEnd - pm.OperationStart;
                }
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //request = TraceMessage(request.CreateBufferedCopy(int.MaxValue));

            Guid correlationId = Guid.NewGuid();

            PerformanceMeasurePoint pm = new PerformanceMeasurePoint();

            pm.OperationName = request.Headers.Action;

            PerformanceMeasures.TryAdd(correlationId, pm);

            pm.CorrelationId = correlationId;

            MessageHeader perfHeader = MessageHeader.CreateHeader("PerformanceCorrelation", "lbDMFManager", correlationId);

            request.Headers.Add(perfHeader);

            if (!sw.IsRunning)
                sw.Start();

            pm.OperationStart = sw.Elapsed;

            MessageHeader perfHeaderElapsed = MessageHeader.CreateHeader("PerformanceElapsedValue", "lbDMFManager", pm.OperationStart);

            request.Headers.Add(perfHeaderElapsed);

            return correlationId;
        }

        #endregion
    }

    public class MessagePerformanceMonitoring : Attribute, IEndpointBehavior, IServiceBehavior
    {
        #region IEndpointBehavior Members

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new MessagePerformanceMonitor());
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MessagePerformanceMonitor());
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IServiceBehavior Members

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
                foreach (EndpointDispatcher endpointDispatcher in cDispatcher.Endpoints)
                    endpointDispatcher.DispatchRuntime.MessageInspectors.Add(
                        new MessagePerformanceMonitor());
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }

}
