using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAPI.DTO
{
    public class ExecuteQueryReq
    {
        [Required]
        public string Query { get; set; }
    }
}
