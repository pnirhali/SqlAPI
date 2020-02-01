using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlAPI.DTO;

namespace SqlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<QueryController> _logger;

        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost("Generate")]
        public IActionResult GenerateQuery(GenerateQueryReq generateQueryReq)
        {
            //1. Generate the query
            string SQL = "use " + generateQueryReq.DatabaseName + "; <br>";

            string operation = generateQueryReq.Operation.ToLower();

            switch (operation)
            {
                case "new":
                    SQL += "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ="
                        + generateQueryReq.TableName + " AND COLUMN_NAME =" + generateQueryReq.ColumnName +
                        ")" + "<br>" + "BEGIN "
                        + "<br>" + "  ALTER TABLE " + generateQueryReq.TableName +
                       "<br>" + " ADD " + generateQueryReq.ColumnName + " " + generateQueryReq.ColumnType + " End;";
                    break;

                case "delete":
                    SQL += "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = "
                        + generateQueryReq.TableName + " AND COLUMN_NAME = " + generateQueryReq.ColumnName +
                                                ")" + "<br>" + "BEGIN "
                        + "<br>" + "  ALTER TABLE " + generateQueryReq.TableName +
                        "<br>" + " DROP " + generateQueryReq.ColumnName + " End;";
                    break;

                case "alter":
                    SQL += "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = "
                        + generateQueryReq.TableName + " AND COLUMN_NAME = " + generateQueryReq.ColumnName +
                                                                       ")" + "<br>" + "BEGIN "
                        + "<br>" + "  ALTER TABLE " + generateQueryReq.TableName +
                      "<br>" + " ALTER COLUMN  " + generateQueryReq.ColumnName + " " + generateQueryReq.ColumnType + " End;";
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
    }
}
