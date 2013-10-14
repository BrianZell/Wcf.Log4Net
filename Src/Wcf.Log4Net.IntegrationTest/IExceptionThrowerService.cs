using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Wcf.Log4Net.IntegrationTest
{
    [ServiceContract]
    interface IExceptionThrowerService
    {
        [OperationContract]
        void ThrowException(string message);
    }
}
