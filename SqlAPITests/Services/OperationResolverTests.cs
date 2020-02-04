using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SqlAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

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

        [TestMethod]
        public void ResolveOperation_For_UnknownOperation_Throws_InvalidOperationException()
        {
            // Mock the dependencies
            var loggerMock = new Mock<ILogger<OperationResolver>>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            // Call and assert
            var operationResolver = new OperationResolver(serviceProviderMock.Object, loggerMock.Object);
            Assert.ThrowsException<InvalidOperationException>(() => operationResolver.ResolveOperation("blah operation"));
        }

        [TestMethod]
        public void ResolveOperation_For_NewOperation_Returns_NewOperation()
        {
            // Mock the dependencies
            var loggerMock = new Mock<ILogger<OperationResolver>>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(m => m.GetService(typeof(NewOperation))).Returns(new NewOperation());

            // Call 
            var operationResolver = new OperationResolver(serviceProviderMock.Object, loggerMock.Object);
            var operation = operationResolver.ResolveOperation("new");

            // Assert
            Assert.IsInstanceOfType(operation, typeof(NewOperation));
        }
    }
}