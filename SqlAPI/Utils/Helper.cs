using Microsoft.EntityFrameworkCore;
using SqlAPI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.Utils
{
    public class Helper
    {

        private static SqlDbContext _dbContext;

        public Helper(SqlDbContext sqlDbContext)
        {
            _dbContext = sqlDbContext;
        }
        public List<T> ExecuteQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                _dbContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                _dbContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                   //create a new DataTable.
                DataTable dt = new DataTable("table");
                   
                    //Load DataReader into the DataTable.
                    dt.Load(result);


                    return dt;
                }
            }
        }
    }
}