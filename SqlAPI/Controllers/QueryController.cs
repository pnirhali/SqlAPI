using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlAPI.Data;
using SqlAPI.DTO;
using SqlAPI.Utils;

namespace SqlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        #region Private Variables

        private readonly ILogger<QueryController> _logger;
        private readonly SqlDbContext _dbContext;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constroctor

        public QueryController(ILogger<QueryController> logger, SqlDbContext sqlDbContext
            , IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = sqlDbContext;
            _configuration = configuration;
        }

        #endregion

        #region Actions

        [HttpPost("Generate")]
        public IActionResult Generate(GenerateQueryReq generateQueryReq)
        {
            //1. Generate the query
            string SQL = string.Empty;

            string operation = generateQueryReq.Operation.ToLower();

            switch (operation)
            {
                case "new":
                    SQL += $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                        $"WHERE TABLE_NAME ={generateQueryReq.TableName} AND COLUMN_NAME ={generateQueryReq.ColumnName})" +
                        $" BEGIN ALTER TABLE {generateQueryReq.TableName} " +
                        $"ADD {generateQueryReq.ColumnName} {generateQueryReq.ColumnType} End;";
                    break;

                case "delete":
                    SQL += $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                        $"WHERE TABLE_NAME = {generateQueryReq.TableName} AND COLUMN_NAME = {generateQueryReq.ColumnName}) " +
                        $"BEGIN  ALTER TABLE {generateQueryReq.TableName} " +
                        $"DROP {generateQueryReq.ColumnName} End; ";
                    break;

                case "alter":
                    SQL += $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                        $"WHERE TABLE_NAME = {generateQueryReq.TableName} AND COLUMN_NAME = {generateQueryReq.ColumnName}) " +
                        $"BEGIN  ALTER TABLE {generateQueryReq.TableName} " +
                        $"ALTER COLUMN  {generateQueryReq.ColumnName} {generateQueryReq.ColumnType} End; ";
                    break;

                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(SQL))
            {
                return BadRequest("Select the correct opertation");
            }

            return Ok(new GenerateQueryRes { SqlQuery = SQL });
        }

        [HttpPost("Execute")]
        public IActionResult Execute(ExecuteQueryReq executeQueryReq)
        {
            Helper helper = new Helper(_dbContext);
            var result = helper.ExecuteQuery(executeQueryReq.Query);
            return Ok(result);
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
