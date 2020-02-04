using SqlAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.Services
{
    public interface IOperation
    {
        string Operation { get; }
        string GenerateQuery(GenerateQueryReq req);
    }
}
