using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wcf.Log4Net.IntegrationTest
{
    class ExceptionThrowerService : IExceptionThrowerService
    {
        public void ThrowException(string message)
        {
            throw new Exception(message);
        }
    }
}
