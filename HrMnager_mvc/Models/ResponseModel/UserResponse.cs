using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Models
{
    public class UserResponse : BaseResponse
    {
        public string Email { get; set; } = default!;
        public string Role{ get; set; } = default!;
        public int? HrManagerId {get; set;}
        public int? EmployeeId {get; set;}
    }
}