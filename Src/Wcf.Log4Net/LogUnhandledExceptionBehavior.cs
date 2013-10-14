using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using log4net;

namespace Wcf.Log4Net
{
    public class LogUnhandledExceptionBehavior : BehaviorExtensionElement, IServiceBehavior
    {
        /// <summary>ErrorHandler</summary>
        public const string ErrorHandlerLogName = "Wcf.Log4Net";

        protected override object CreateBehavior()
        {
            return new LogUnhandledExceptionBehavior();
        }

        public override Type BehaviorType
        {
            get { return this.GetType(); }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //do nothing.
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            //do nothing.
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            ILog log = LogManager.GetLogger(ErrorHandlerLogName);
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                chanDisp.ErrorHandlers.Add(new Log4NetErrorHandler(log));
            }
        }
    }
}
