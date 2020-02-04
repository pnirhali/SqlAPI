using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.DTO
{
    public class ExecuteQueryRes
    {
        public List<string> ColumnHeaders { get; set; }
        public DataTable Rows { get; set; }
    }
}
