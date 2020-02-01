using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.DTO
{
    public class GenerateQueryReq
    {
        [Required]
        public string DatabaseName { get; set; }

        [Required]
        public string TableName { get; set; }
        
        [Required]
        public string ColumnName { get; set; }
        
        [Required]
        public string Operation { get; set; }

        [Required]
        public string ColumnType { get; set; }

    }
}
