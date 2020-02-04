using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.Services
{
    public class OperationResolver : IOperationResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OperationResolver> _logger;

        public OperationResolver(IServiceProvider serviceProvider, ILogger<OperationResolver> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IOperation ResolveOperation(string operationName)
        {
            // Vaildate
            if (string.IsNullOrWhiteSpace(operationName))
            {
                throw new ArgumentNullException(nameof(operationName));
            }

            // Resolve
            switch (operationName)
            {
                case "new": return _serviceProvider.GetService<NewOperation>();
                case "delete": return _serviceProvider.GetService<DeleteOperation>();
                case "alter": return _serviceProvider.GetService<AlterOperation>();

                default:
                    _logger.LogError($"Unkown operation : {operationName}");
                    throw new InvalidOperationException(operationName);
            };
        }
    }
}
