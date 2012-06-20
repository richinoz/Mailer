using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExceptionMailer.Core.Data.Context;
using FamineDto.Enums;
using FamineDto.Infrastructure;
using FamineDto.Models;
using NUnit.Framework;

namespace ExceptionMailer.Tests
{
    /// <summary>
    /// ensure connection to test database works
    /// </summary>
    [TestFixture]
    public class DatabaseIntegrationTests
    {
        [Test]
        public void Can_Save_TxProcEchoWebServiceRequests()
        {
            //var transactionDomainContext = new TransactionDomainContext("FamineWebsite");

            //var echoWebServiceRequest = transactionDomainContext.EchoWebServiceRequests.First();

            //Assert.That(echoWebServiceRequest, Is.Not.Null);


        }


    }
}
