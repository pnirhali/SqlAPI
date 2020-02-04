using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SqlAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlAPI.Services.Tests
{
    [TestClass]
    public class OperationResolverTests
    {
        [TestMethod]
        public void ResolveOperation_For_NullOperation_Throws_ArgumentNullException()
        {
            // Mock the dependencies
            var loggerMock = new Mock<ILogger<OperationResolver>>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            // Call and assert
            var operationResolver = new OperationResolver(serviceProviderMock.Object, loggerMock.Object);
            Assert.ThrowsException<ArgumentNullException>(() => operationResolver.ResolveOperation(null));
        }
    }
}