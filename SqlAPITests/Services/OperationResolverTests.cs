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

        [DataTestMethod]
        [DataRow("new", typeof(NewOperation))]
        [DataRow("alter", typeof(AlterOperation))]
        [DataRow("delete", typeof(DeleteOperation))]
        public void ResolveOperation_For_ValidOperation_Returns_IOperation(string operationName, Type operationType)
        {
            // Mock the dependencies
            var loggerMock = new Mock<ILogger<OperationResolver>>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(m => m.GetService(operationType)).Returns(Activator.CreateInstance(operationType));

            // Call 
            var operationResolver = new OperationResolver(serviceProviderMock.Object, loggerMock.Object);
            var operation = operationResolver.ResolveOperation(operationName);

            // Assert
            Assert.IsInstanceOfType(operation, operationType); // exact instance
            Assert.IsInstanceOfType(operation, typeof(IOperation)); // interface
        }
    }
}