using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace Wcf.Log4Net.IntegrationTest
{
    class UnhandledExceptionBehaviorTest
    {
        [Test]
        public void Log4NetCatchesUnhandledExceptionsTest()
        {
            // Arrange
            const string exceptionMessage = "myMessage";
            var testUri = new Uri("net.pipe://localhost/ExceptionThrower");

            var memoryAppender = new MemoryAppender();
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.AddAppender(memoryAppender);
            hierarchy.Root.Level = Level.Error;
            hierarchy.Configured = true;
            memoryAppender.Clear();

            var serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(IExceptionThrowerService)),
                                                      new NetNamedPipeBinding(),
                                                      new EndpointAddress(testUri));

            var factory =
                    new ChannelFactory<IExceptionThrowerService>(serviceEndpoint);

            // Act
            using (var host = new ServiceHost(typeof (ExceptionThrowerService)))
            {
                host.AddServiceEndpoint(serviceEndpoint);
                host.Description.Behaviors.Add(new LogUnhandledExceptionBehavior());
                host.Open();

                try
                {
                    var service = factory.CreateChannel();
                    service.ThrowException(exceptionMessage);
                }
                catch(FaultException) 
                {
                    //swallow the returned exception (we know it's coming back)
                }

                host.Close();
            }
            
            // Assert
            var events = memoryAppender.GetEvents();
            Assert.That(events.Count(), Is.GreaterThan(0));
            Assert.That(events.Last().ExceptionObject.Message, Is.EqualTo(exceptionMessage));
            //Display received exceptions
            events.Select(x => x.ExceptionObject.ToString()).ToList().ForEach(Console.WriteLine);
        }
        
    }
}
