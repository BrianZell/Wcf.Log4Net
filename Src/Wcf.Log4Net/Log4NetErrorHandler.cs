using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using log4net;

namespace Wcf.Log4Net
{
    public class Log4NetErrorHandler : IErrorHandler
    {
        private readonly ILog _log;

        public Log4NetErrorHandler(ILog log)
        {
            _log = log;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            //do nothing.
        }

        public bool HandleError(Exception error)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error("Unhandled Exception", error);
            }
            return true;
        }
    }
}