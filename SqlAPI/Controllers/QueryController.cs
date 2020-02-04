using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlAPI.Data;
using SqlAPI.DTO;
using SqlAPI.Services;
using SqlAPI.Utils;

namespace SqlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        #region Local dependencies

        private readonly ILogger<QueryController> _logger;
        private readonly SqlDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IOperationResolver _operationResolver;

        #endregion

        public QueryController(ILogger<QueryController> logger, SqlDbContext sqlDbContext
            , IConfiguration configuration, IOperationResolver operationResolver)
        {
            _logger = logger;
            _dbContext = sqlDbContext;
            _configuration = configuration;
            _operationResolver = operationResolver;
        }

        #region APIs

        [HttpPost("Generate")]
        public IActionResult Generate(GenerateQueryReq generateQueryReq)
        {
            string operationName = generateQueryReq.Operation.ToLowerInvariant();
            _logger.LogInformation($"Executing {operationName}");
            var res = new GenerateQueryRes();
            var operation = _operationResolver.ResolveOperation(operationName);
            res.SqlQuery = operation.GenerateQuery(generateQueryReq);
            return Ok(res);
        }

        [HttpPost("Execute")]
        public IActionResult Execute(ExecuteQueryReq executeQueryReq)
        {
            var helper = new Helper(_dbContext);
            var dataset = helper.ExecuteQuery(executeQueryReq.Query);
            var colHeaders = new List<string>();
            foreach (DataColumn col in dataset.Columns)
            {
                var name = col.ColumnName;
                var camelCaseName = char.ToLowerInvariant(name[0]) + name.Substring(1);
                colHeaders.Add(camelCaseName);
            }
            return Ok(new { ColumnHeaders = colHeaders, Rows = dataset });
        }

        [HttpGet("Databases")]
        public IActionResult GetDatabases()
        {
            var response = new DatabasesRes();
            var databases = new List<string>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            databases.Add(dr[0].ToString());
                        }
                        response.Names = databases;
                    }
                }
                return Ok(response);

            }

        }

        #endregion

    }
}
