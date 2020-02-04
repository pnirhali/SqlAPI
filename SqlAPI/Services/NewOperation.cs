using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlAPI.DTO;

namespace SqlAPI.Services
{
    public class NewOperation : IOperation
    {
        public string Operation => "new";

        public string GenerateQuery(GenerateQueryReq req)
        {
            // Validate
            if (req == null || req.Operation != Operation)
            {
                throw new InvalidOperationException();
            }

            // Form query
            return $"IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS" +
                 $" WHERE TABLE_NAME =[{req.TableName}] AND COLUMN_NAME =[{req.ColumnName}] )" +
                 $" BEGIN ALTER TABLE [{req.TableName}]" +
                 $" ADD [{req.ColumnName}] {req.ColumnType} End;";
        }
    }
}
