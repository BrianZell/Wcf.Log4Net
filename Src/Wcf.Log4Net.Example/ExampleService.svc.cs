using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Wcf.Log4Net.Example
{
    public class ExampleService : IExampleService
    {
        public void ThrowException()
        {
            throw new InvalidOperationException("Wcf.Log4Net.Example exception thrown!");
        }
    }
}
