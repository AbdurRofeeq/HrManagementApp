using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Models
{
    public class BaseResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}