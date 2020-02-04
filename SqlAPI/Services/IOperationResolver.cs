using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.Services
{
    public interface IOperationResolver
    {
        IOperation ResolveOperation(string operationName);
    }
}
