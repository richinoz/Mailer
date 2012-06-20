
using System.Collections.Generic;
using EchoWebService.Core.EchoWebServiceSearch;
using EchoWebService.Core.Services;
using ExceptionMailer.Core.Commands.Concrete;
using ExceptionMailer.Core.Email;
using ExceptionMailer.Infrastructure;
using Moq;
using NUnit.Framework;

namespace ExceptionMailer.Tests
{
    /// <summary>
    /// ensure connection to test database works
    /// </summary>
    [TestFixture]
    public class EchoSearchTimerPlugInTests
    {
        private Mock<IExceptionEmailSender> exceptionMailer = new Mock<IExceptionEmailSender>();
        private Mock<ISmsSender> smsSender = new Mock<ISmsSender>();
        private Mock<IInternalErrorMailSender> internalErrorMailer = new Mock<IInternalErrorMailSender>();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Error_Sends_Email()
        {
            var echoService = new Mock<IEchoSearchService>();

            var wsbSupporters = new List<WSBSupporter>();
            wsbSupporters.Add(new WSBSupporter() { Message = new Messages() { ApplicationMessage = ApplicationMessage.Failed } });

            echoService.Setup(x => x.SearchSupporterGroup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(wsbSupporters);

            var echoSearchTimerPlugIn = new EchoSearchTimerPlugIn(exceptionMailer.Object, smsSender.Object, internalErrorMailer.Object, echoService.Object);

            echoSearchTimerPlugIn.DoInvoke();

            smsSender.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
            exceptionMailer.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));

        }


    }
}