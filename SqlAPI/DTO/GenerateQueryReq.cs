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
        [MinLength(1)]
        public string DatabaseName { get; set; }

        [Required]
        [MinLength(1)]
        public string TableName { get; set; }
        
        [Required]
        [MinLength(1)]
        public string ColumnName { get; set; }
        
        [Required]
        [MinLength(1)]
        public string Operation { get; set; }

        [Required]
        public string ColumnType { get; set; }

    }
}
